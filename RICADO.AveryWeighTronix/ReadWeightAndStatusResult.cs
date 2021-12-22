using System;

namespace RICADO.AveryWeighTronix
{
    public class ReadWeightAndStatusResult : RequestResult
    {
        #region Private Fields

        private readonly bool _stableStatus;
        private readonly double _weight;
        private readonly WeightType _type;
        private readonly UnitOfMeasure _units;
        private readonly bool _outOfRange;

        #endregion


        #region Public Properties

        public bool StableStatus => _stableStatus;
        public double Weight => _weight;
        public WeightType Type => _type;
        public UnitOfMeasure Units => _units;
        public bool OutOfRange => _outOfRange;

        #endregion


        #region Constructor

        internal ReadWeightAndStatusResult(Channels.ProcessMessageResult result, bool stableStatus, double weight, WeightType type, UnitOfMeasure units, bool outOfRange) : base(result)
        {
            _stableStatus = stableStatus;
            _weight = weight;
            _type = type;
            _units = units;
            _outOfRange = outOfRange;
        }

        #endregion
    }
}
