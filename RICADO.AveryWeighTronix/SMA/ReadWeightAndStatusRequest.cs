using System;
using System.Text;

namespace RICADO.AveryWeighTronix.SMA
{
    internal class ReadWeightAndStatusRequest : Request
    {
        #region Constructor

        protected ReadWeightAndStatusRequest(string commandCode) : base(commandCode)
        {
        }

        #endregion


        #region Public Methods

#if NETSTANDARD
        public ReadWeightAndStatusResponse UnpackResponseMessage(byte[] responseMessage)
        {
            return ReadWeightAndStatusResponse.UnpackResponseMessage(this, responseMessage);
        }
#else
        public ReadWeightAndStatusResponse UnpackResponseMessage(Memory<byte> responseMessage)
        {
            return ReadWeightAndStatusResponse.UnpackResponseMessage(this, responseMessage);
        }
#endif

        public static ReadWeightAndStatusRequest CreateNew(AveryWeighTronixDevice device)
        {
            return new ReadWeightAndStatusRequest(Commands.ReadWeightAndStatus);
        }

        #endregion


        #region Protected Methods

        protected override void BuildMessageDetail(ref StringBuilder messageBuilder)
        {
        }

        #endregion
    }
}
