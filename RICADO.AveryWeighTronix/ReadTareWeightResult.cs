using System;

namespace RICADO.AveryWeighTronix
{
    public class ReadTareWeightResult : RequestResult
    {
        #region Private Fields

        private readonly double _tareWeight;
        private readonly UnitOfMeasure _units;

        #endregion


        #region Public Properties

        public double TareWeight => _tareWeight;
        public UnitOfMeasure Units => _units;

        #endregion


        #region Constructor

        internal ReadTareWeightResult(Channels.ProcessMessageResult result, double tareWeight, UnitOfMeasure units) : base(result)
        {
            _tareWeight = tareWeight;
            _units = units;
        }

        #endregion
    }
}
