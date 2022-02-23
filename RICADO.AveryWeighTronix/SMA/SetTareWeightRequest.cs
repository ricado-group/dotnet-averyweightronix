using System;
using System.Text;

namespace RICADO.AveryWeighTronix.SMA
{
    internal class SetTareWeightRequest : Request
    {
        #region Private Fields

        private readonly double _tareWeight;

        #endregion


        #region Public Properties

        public double TareWeight => _tareWeight;

        #endregion


        #region Constructor

        protected SetTareWeightRequest(string commandCode, double tareWeight) : base(commandCode)
        {
            _tareWeight = tareWeight;
        }

        #endregion


        #region Public Methods

#if NETSTANDARD
        public void ValidateResponseMessage(byte[] responseMessage)
#else
        public void ValidateResponseMessage(Memory<byte> responseMessage)
#endif
        {
            SetTareWeightResponse.ValidateResponseMessage(this, responseMessage);
        }

        public static SetTareWeightRequest CreateNew(AveryWeighTronixDevice device, double tareWeight)
        {
            return new SetTareWeightRequest(Commands.SetTareWeight, tareWeight);
        }

        #endregion


        #region Protected Methods

        protected override void BuildMessageDetail(ref StringBuilder messageBuilder)
        {
            if(_tareWeight <= 0 || _tareWeight > 99999999.9)
            {
                messageBuilder.Append("0".PadLeft(10, '\u0020'));
            }
            else
            {
                messageBuilder.Append(Math.Round(_tareWeight, 1).ToString().PadLeft(10, '\u0020'));
            }
        }

        #endregion
    }
}
