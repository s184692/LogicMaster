using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicMaster.generator
{
    public class GameSettings
    {
        public enum DifficultyPresets { Easy, Medium, Hard };

        // ---Configuration constants---
        private static readonly (int MIN, int MAX) TARGET_COUNT = (4, 6);

        private static readonly (int MIN, int MAX) LAYER_COUNT = (3, 3);

        private static readonly (double MIN, double MAX) MERGE_CHANCE = (1.0, 1.0);

        private static readonly (double MIN, double MAX) MISSING_GATES = (0.0, 1.0);

        private int _targetCount; // amount of logic targets (lamps)

        private int _layerCount; // complexity of circuit; number of gate layers

        private double _mergeChance; // complexity of circuit; chance of merging inputs between 2 gates

        private double _missingGates; // fraction of gates missing from circuit

        public int TargetCount
        {
            get 
            {
                return _targetCount;
            }
            set
            {
                _targetCount = Math.Clamp(value, TARGET_COUNT.MIN, TARGET_COUNT.MAX);
            }
        }

        public int LayerCount
        {
            get
            {
                return _layerCount;
            }
            set
            {
                _layerCount = Math.Clamp(value, LAYER_COUNT.MIN, LAYER_COUNT.MAX);
            }
        }

        public double MergeChance
        {
            get
            {
                return _mergeChance;
            }
            set
            {
                _mergeChance = Math.Clamp(value, MERGE_CHANCE.MIN, MERGE_CHANCE.MAX);
            }
        }

        public double MissingGates
        {
            get
            {
                return _missingGates;
            }
            set
            {
                _missingGates = Math.Clamp(value, MISSING_GATES.MIN, MISSING_GATES.MAX);
            }
        }

        // default constructor is also a medium difficulty preset
        public GameSettings()
        {
            TargetCount = (TARGET_COUNT.MIN + TARGET_COUNT.MAX) / 2;
            LayerCount = (LAYER_COUNT.MIN + LAYER_COUNT.MAX) / 2;
            MissingGates = (MISSING_GATES.MIN + MISSING_GATES.MAX) / 2;
            MergeChance = (MERGE_CHANCE.MIN + MERGE_CHANCE.MAX) / 2;
        }

        public GameSettings(DifficultyPresets preset) : this()
        {
            switch (preset)
            {
                case DifficultyPresets.Easy:
                    TargetCount = TARGET_COUNT.MIN;
                    LayerCount = LAYER_COUNT.MIN;
                    MissingGates = (MISSING_GATES.MIN + MISSING_GATES.MAX) / 2;
                    MergeChance = MERGE_CHANCE.MIN;
                    break;
                case DifficultyPresets.Medium:
                    break;
                case DifficultyPresets.Hard:
                    TargetCount = TARGET_COUNT.MAX;
                    LayerCount = LAYER_COUNT.MAX;
                    MissingGates = MISSING_GATES.MAX;
                    MergeChance = MERGE_CHANCE.MAX;
                    break;
                default:
                    break;
            }
        }
    }
}
