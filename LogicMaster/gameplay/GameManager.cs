using LogicMaster.gameplay.logic;
using LogicMaster.gameplay.visuals;
using LogicMaster.generator;
using LogicMaster.gui.controls;
using LogicMaster.gui.dialog;
using LogicMaster.gui.pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace LogicMaster.gameplay
{
    /// <summary>
    /// Menadzer gry
    /// </summary>
    public class GameManager
    {
        private Game game { get; set; }

        private int time { get; set; }

        private Timer timer { get; }

        private GameInventory inventory { get; set; }

        private List<List<LogicContainer>> containerLayers = null;

        private int? Seed { get; set; } = null;

        private GameSettings? Settings { get; set; } = null;

        public bool GameStarted { get; private set; }

        public GameManager(Game game)
        {
            this.game = game;
            this.time = 0;
            timer = new Timer(1000.0)
            {
                AutoReset = true,
                Enabled = false,
            };
            inventory = new GameInventory(this);
            GameStarted = false;
        }
        /// <summary>
        /// Tworzenie nowej gry
        /// </summary>
        /// <param name="settings">Ustawienia gry</param>
        /// <param name="seed">Ziarno generatora</param>
        public void LoadNewGame(GameSettings settings, int? seed = null)
        {
            if (GameStarted)
                ClearPreviousGame();

            GameGenerator generator;
            Settings = settings; // save settings
            Seed = seed;
            if (Seed == null)
            {
                generator = new GameGenerator(Settings);
                Seed = generator.Seed;
            }
            else
            {
                generator = new GameGenerator(Settings, Seed.Value);
            }
            game.levelLabel.Content = $"GAME#{Seed}";

            List<List<(Type type, int input, bool state, bool merged, bool missing)>> layers = generator.GenerateGameCircuit();
            containerLayers = new List<List<LogicContainer>>();
            Dictionary<string, int> amounts = new Dictionary<string, int>()
            {
                { "BUF", 0 },
                { "NOT", 0 },
                { "AND", 0 },
                { "NAND", 0 },
                { "OR", 0 },
                { "NOR", 0 },
                { "XOR", 0 },
                { "XNOR", 0 }
            };

            int height = layers.Count;
            int width = 0;
            foreach (List<(Type, int, bool, bool, bool)> layer in layers)
                width = layer.Count > width ? layer.Count : width;
            game.gameCanvas.containerSizeModifier = Math.Max(width, height);

            for (int i = 0; i < height; i++)
            {
                int layerWidth = layers[i].Count;
                int gateIndex = 0;
                int inputsLeft = 0;
                containerLayers.Add(new List<LogicContainer>());

                //Debug.WriteLine($"Layer {i}:");
                //Debug.WriteLine($"{"Type:",12}|{"Input:",6}|{"State:",6}|{"Merge:",6}|{"Miss:",6}");
                //foreach (var e in layers[i])
                //    Debug.WriteLine($"{e.type.Name,12}|{e.input,6}|{e.state,6}|{e.merged,6}|{e.missing,6}");

                for (int j = 0; j < layerWidth; j++) // for each gate
                {
                    Point pos = new Point((double)(j + 1) / (double)(layerWidth + 1), (double)(i + 1) / (double)(height + 1));
                    LogicContainer logicContainer = new LogicContainer();
                    logicContainer.PropertyChanged += LogicContainer_PropertyChanged;

                    if (i > 0) // connect to upper layers
                    {
                        if (gateIndex >= layers[i - 1].Count) // ???
                            break;

                        if (inputsLeft <= 0)
                            inputsLeft = layers[i - 1][gateIndex].input;

                        logicContainer.ConnectTo(containerLayers[i - 1][gateIndex]);
                        inputsLeft--;
                        if (inputsLeft <= 0)
                        {
                            gateIndex++;
                            if (gateIndex < layers[i - 1].Count && layers[i - 1][gateIndex - 1].merged)
                            {
                                inputsLeft = layers[i - 1][gateIndex].input;
                                if (inputsLeft - 1 > 0)
                                {
                                    logicContainer.ConnectTo(containerLayers[i - 1][gateIndex]);
                                    inputsLeft--;
                                }
                            }
                        }
                    }

                    if (!layers[i][j].missing) // add logic element instance if not missing
                        logicContainer.SetAndAttachLogicElement(GetLogicElementFromLayerData(layers[i][j]));
                    else // otherwise add it to inventory
                        amounts[layers[i][j].type.Name]++;

                    containerLayers[i].Add(logicContainer);
                    game.gameCanvas.AddContainer(logicContainer, pos);
                }
            }

            inventory.InitializeInventory(amounts);
            AddInventoryBoxes();
            GameStarted = true;
            game.gameCanvas.UpdateVisuals();
            UpdateGameGoal();
        }

        private void LogicContainer_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            GateObject? go = sender as GateObject;
            if (go != null && e.PropertyName == "Parent")
            {
                inventory.AddGateBackToInventory(go.logicGate.Name);
                return;
            }

            LogicContainer? lc = sender as LogicContainer;
            if (lc != null && e.PropertyName == "State")
                if (lc.logicElement?.GetType() == typeof(LogicTarget))
                    UpdateGameGoal();
        }

        private LogicElement GetLogicElementFromLayerData((Type type, int input, bool state, bool merged, bool missing) data)
        {
            if (data.type == typeof(LogicSource))
            {
                return new LogicSource(data.state);
            }
            else if (data.type == typeof(LogicTarget))
            {
                return new LogicTarget();
            }
            else
            {
                return (LogicElement)Activator.CreateInstance(data.type);
            }
        }
        /// <summary>
        /// Dodawanie miejsc w wyposazeniu
        /// </summary>
        private void AddInventoryBoxes()
        {
            foreach (var (key, value) in inventory.InventoryBoxes)
                game.inventoryGrid.Children.Add(value);
        }
        /// <summary>
        /// Czyszczenie poprzedniej gry
        /// </summary>
        private void ClearPreviousGame()
        {
            GameStarted = false;

            game.inventoryGrid.Children.Clear();
            inventory.ClearInventory();
            
            ResetTime();

            game.gameCanvas.ClearAllData();

            foreach (List<LogicContainer> layer in containerLayers)
            {
                foreach (LogicContainer lc in layer)
                    lc.PropertyChanged -= LogicContainer_PropertyChanged;
                layer.Clear();
            }
            containerLayers.Clear();
                
        }
        /// <summary>
        /// Funkcja restartu gry
        /// </summary>
        public void RestartCurrentGame()
        {
            if (Settings != null && Seed != null)
                LoadNewGame(Settings, Seed);
        }
        /// <summary>
        /// Funkcja sprawdzajaca stan rozgrywki
        /// </summary>
        private async void  UpdateGameGoal()
        {
            if (GameStarted)
            {
                // logic target layer
                int targets = containerLayers[0].Count;
                int on = 0;
                foreach (LogicContainer lc in containerLayers[0])
                    if (lc.logicElement.State)
                        on++;
                game.levelProgressBar.Value = (double)on / (double)targets;

                if (on == targets)
                {
                    StopTimer();
                    MainWindow? mainWindow = App.Current.MainWindow as MainWindow;
                    if (mainWindow != null)
                    {
                        // prevent multiple game won windows
                        foreach (Window w in mainWindow.OwnedWindows)
                            if (w is GameWonWindow)
                                return;

                        GameWonWindow window = new GameWonWindow(time);
                        window.Owner = mainWindow;
                        mainWindow.contentOverlay.Opacity = 0.5;
                        await Task.Delay(100);
                        window.ShowDialog();
                        mainWindow.contentOverlay.Opacity = 0.0;

                        ClearPreviousGame();
                        mainWindow.LoadMainMenu();
                    }
                }
            }
        }
        /// <summary>
        /// Funkcja rozpoczynajaca odliczanie stopera
        /// </summary>
        public void StartTimer()
        {
            if (!timer.Enabled)
            {
                timer.Elapsed += Timer_Elapsed;
                timer.Enabled = true;
            }
        }
        /// <summary>
        /// Funkcja konczaca odliczanie stopera
        /// </summary>
        public void StopTimer()
        {
            if (timer.Enabled)
            {
                timer.Elapsed -= Timer_Elapsed;
                timer.Enabled = false;
            }
        }
        /// <summary>
        /// Resetowanie czasu
        /// </summary>
        private void ResetTime()
        {
            time = 0;
            game.timeLabel.Content = $"{0:00}:{0:00}";
        }
      
        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            time++;
            game.Dispatcher.Invoke(() => { game.timeLabel.Content = $"{(time / 60) % 100:00}:{time % 60:00}"; });
        }
    }
}
