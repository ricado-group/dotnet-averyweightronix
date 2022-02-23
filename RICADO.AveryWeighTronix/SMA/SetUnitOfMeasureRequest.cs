using System;
using System.Text;

namespace RICADO.AveryWeighTronix.SMA
{
    internal class SetUnitOfMeasureRequest : Request
    {
        #region Private Fields

        private readonly UnitOfMeasure _newUnits;

        #endregion


        #region Public Properties

        public UnitOfMeasure NewUnits => _newUnits;

        #endregion


        #region Constructor

        protected SetUnitOfMeasureRequest(string commandCode, UnitOfMeasure newUnits) : base(commandCode)
        {
            _newUnits = newUnits;
        }

        #endregion


        #region Public Methods

#if NETSTANDARD
        public void ValidateResponseMessage(byte[] responseMessage)
#else
        public void ValidateResponseMessage(Memory<byte> responseMessage)
#endif
        {
            SetUnitOfMeasureResponse.ValidateResponseMessage(this, responseMessage);
        }

        public static SetUnitOfMeasureRequest CreateNew(AveryWeighTronixDevice device, UnitOfMeasure newUnits)
        {
            return new SetUnitOfMeasureRequest(Commands.SetUnitOfMeasure, newUnits);
        }

        #endregion


        #region Protected Methods

        protected override void BuildMessageDetail(ref StringBuilder messageBuilder)
        {
            messageBuilder.Append(_newUnits.ToMessageString());
        }

        #endregion
    }
}
