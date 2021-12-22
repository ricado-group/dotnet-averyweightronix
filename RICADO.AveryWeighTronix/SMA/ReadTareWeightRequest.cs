using System;
using System.Text;

namespace RICADO.AveryWeighTronix.SMA
{
    internal class ReadTareWeightRequest : Request
    {
        #region Constructor

        protected ReadTareWeightRequest(string commandCode) : base(commandCode)
        {
        }

        #endregion


        #region Public Methods

        public ReadTareWeightResponse UnpackResponseMessage(Memory<byte> responseMessage)
        {
            return ReadTareWeightResponse.UnpackResponseMessage(this, responseMessage);
        }

        public static ReadTareWeightRequest CreateNew(AveryWeighTronixDevice device)
        {
            return new ReadTareWeightRequest(Commands.ReadTareWeight);
        }

        #endregion


        #region Protected Methods

        protected override void BuildMessageDetail(ref StringBuilder messageBuilder)
        {
        }

        #endregion
    }
}
