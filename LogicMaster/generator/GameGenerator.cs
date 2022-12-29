using LogicMaster.gameplay.logic;
using LogicMaster.gameplay.logic.gates;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LogicMaster.generator
{
    public class GameGenerator
    {
        // class for representing gate data during generating
        private class Gate
        {
            public Type Type { get; set; }
            public bool[] TruthTable { get; set; }
            public int InputCount { get { return BitOperations.Log2((uint)TruthTable.Length); } }

            public bool[][] Inputs
            {
                get
                {
                    bool[][] inputs = new bool[TruthTable.Length][];

                    for (int i = 0; i < TruthTable.Length; i++)
                    {
                        inputs[i] = new bool[InputCount];
                        for (int j = 0; j < InputCount; j++)
                        {
                            inputs[i][InputCount - j - 1] = ((i >> j) & 1) > 0;
                        }
                    }

                    return inputs;
                }
            }
            public Gate(Type type, bool[] truthTable)
            {
                Type = type;
                TruthTable = truthTable;
            }
        }

        // class for representing logic data during generating
        private class Logic
        {
            public bool State { get; set; }
            public bool IsMerged { get; set; }

            public Logic(bool state, bool isMerged = false)
            {
                State = state;
                IsMerged = isMerged;
            }
        }

        private static readonly Gate[] gateList = GetGateList();

        private Random random { get; }

        private GameSettings settings { get; }

        public GameGenerator()
        {
            random = new Random();
            settings = new GameSettings();
        }

        public GameGenerator(GameSettings gameSettings)
        {
            random = new Random();
            settings = gameSettings;
        }

        public GameGenerator(GameSettings gameSettings, int seed)
        {
            random = new Random(seed);
            settings = gameSettings;
        }

        // list of layers of logic elements: 
        // Type - what kind of LogicElement is it
        // int - how many inputs it has (0,1,2)
        // bool - what state should it be (needed for LogicSource)
        // bool - is output merged (whether output should be connected to one or two elements in layer above)
        // bool - is gate missing from circuit (fields for player to fill)
        public List<List<(Type, int, bool, bool, bool)>> GenerateGameCircuit()
        {
            return PrepareLogicCircuit(GenerateLogicCircuit());
        }

        private List<List<(Type, int, bool, bool, bool)>> PrepareLogicCircuit((Logic[] logicLayer, Gate[]? gateLayer)[] circuit)
        {
            List<List<(Type, int, bool, bool, bool)>> layerList = new List<List<(Type, int, bool, bool, bool)>>();

            int gateCount = 0;
            for (int i = 0; i < circuit.Length - 1; i++)
                gateCount += circuit[i].gateLayer.Length; // count how many gates there will be
            int missingCount = (int)Math.Round(gateCount * settings.MissingGates); // count how many gates need to be missing

            Logic[] targetLayer = circuit.First().logicLayer;
            List<(Type, int, bool, bool, bool)> targetList = new List<(Type, int, bool, bool, bool)>();
            foreach (Logic target in targetLayer)
                targetList.Add((typeof(LogicTarget), 1, target.State, target.IsMerged, false));
            layerList.Add(targetList);

            for (int i = 0; i < circuit.Length - 1; i++)
            {
                Gate[] gateLayer = circuit[i].gateLayer;
                Logic[] gateStateLayer = circuit[i + 1].logicLayer;
                List<(Type, int, bool, bool, bool)> gateList = new List<(Type, int, bool, bool, bool)>();

                int stateIndex = 0;
                foreach(Gate gate in gateLayer)
                {
                    bool isMissing = random.NextSingle() < (double)missingCount / (double)gateCount;
                    if (gateStateLayer[stateIndex + gate.InputCount - 1].IsMerged)
                    {
                        gateList.Add((gate.Type, gate.InputCount, false, true, isMissing));
                        stateIndex += gate.InputCount - 1;
                    }
                    else
                    {
                        gateList.Add((gate.Type, gate.InputCount, false, false, isMissing));
                        stateIndex += gate.InputCount;
                    }
                    missingCount -= isMissing ? 1 : 0;
                    gateCount--;
                }

                layerList.Add(gateList);
            }

            Logic[] sourceLayer = circuit.Last().logicLayer;
            List<(Type, int, bool, bool, bool)> sourceList = new List<(Type, int, bool, bool, bool)>();
            foreach (Logic source in sourceLayer)
                sourceList.Add((typeof(LogicSource), 0, source.State, false, false));
            layerList.Add(sourceList);

            return layerList;
        }

        private (Logic[] logicLayer, Gate[]? gateLayer)[] GenerateLogicCircuit()
        {
            (Logic[] logicLayer, Gate[]? gateLayer)[] layers = new (Logic[], Gate[]?)[settings.LayerCount];
            layers[0].logicLayer = GetRandomLogicLayer(settings.TargetCount); // layer of targets
            layers[settings.LayerCount - 1].gateLayer = null; // layer of sources

            for (int i = 0; i < settings.LayerCount - 1; i++)
            {
                layers[i].gateLayer = GetRandomGateLayer(layers[i].logicLayer.Length);
                if (layers[i].gateLayer != null)
                {
                    Logic[]? tempLayer = GetLogicLayerFromUpperLayer(layers[i].gateLayer, layers[i].logicLayer); // try to get layer with merge enabled
                    if (tempLayer == null)
                        tempLayer = GetLogicLayerFromUpperLayer(layers[i].gateLayer, layers[i].logicLayer, false); // try with merge disabled, it should be always possible (i think)
                    if (tempLayer == null)
                        throw new Exception("Generation error: couldn't generate logic layer.");
                    layers[i + 1].logicLayer = tempLayer;
                }
                else
                    throw new Exception("Generation error: couldn't generate gate layer.");
            }

            return layers;
        }

        private Logic[] GetRandomLogicLayer(int n)
        {
            Logic[] layer = new Logic[n];

            for (int i = 0; i < layer.Length; i++)
            {
                layer[i] = new Logic(random.NextSingle() < 0.5);
            }

            return layer;
        }

        private Gate[] GetRandomGateLayer(int n)
        {
            Gate[] gates = new Gate[n];

            for (int i = 0; i < n; i++)
            {
                gates[i] = GetRandomGate();
            }

            return gates;
        }

        private Logic[]? GetLogicLayerFromUpperLayer(Gate[] gateLayer, Logic[] logicLayer, bool mergeEnabled = true)
        {
            List<List<Logic>> prevList = new List<List<Logic>>();

            for (int i = 0; i < gateLayer.Length; i++)
            {
                List<List<Logic>> nextList = new List<List<Logic>>();

                bool merge = mergeEnabled && prevList.Count > 0 && random.NextSingle() < settings.MergeChance ; // decide if to merge
                bool state = logicLayer[i].State; // state of output of a gate
                bool[] truthtable = gateLayer[i].TruthTable; // truthtable for that gate
                bool[][] inputs = gateLayer[i].Inputs; // possible input patterns for that gate corresponding to truthtable

                foreach (List<Logic> prev in prevList)
                {
                    for (int j = 0; j < truthtable.Length; j++) // for every input pattern of a gate
                    {
                        // if next gate is meant to have first input merged
                        if (merge)
                        {
                            // if this pattern will result in correct state and right input of gate to the left matches left input of this gate
                            if (truthtable[j] == state && prev.Last().State == inputs[j][0])
                            {
                                List<Logic> temp = inputs[j][1..].ToList().ConvertAll(state => new Logic(state));
                                prev.Last().IsMerged = true;
                                nextList.Add(prev.Concat(temp).ToList());
                            }
                        }
                        else
                        {
                            // if this pattern will result in correct state
                            if (truthtable[j] == state)
                            {
                                List<Logic> temp = inputs[j].ToList().ConvertAll(state => new Logic(state));
                                nextList.Add(prev.Concat(temp).ToList());
                            }
                        }
                    }
                }

                if (nextList.Count > 0)
                {
                    prevList = nextList;
                }
                else
                {
                    return null;
                }
            }

            return prevList[random.Next(prevList.Count)].ToArray();
        }

        // gets list of all classes derived from LogicGate and its number of inputs
        private static Gate[] GetGateList()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<Gate> gates = new List<Gate>();

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(LogicGate)))
                {
                    PropertyInfo? propertyInfo = type.GetProperty("TruthTable");
                    if (propertyInfo != null)
                    {
                        LogicGate? instance = Activator.CreateInstance(type) as LogicGate;
                        bool[]? truthTable = instance?.TruthTable;
                        if (truthTable != null)
                        {
                            gates.Add(new Gate(type, truthTable));
                        }
                    }
                }
            }

            return gates.ToArray();
        }

        private Gate GetRandomGate()
        {
            return gateList[random.Next(gateList.Length)];
        }
    }
}
