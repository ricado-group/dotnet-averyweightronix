using System;
using System.Text;

namespace RICADO.AveryWeighTronix.SMA
{
    internal class ZeroCommandRequest : Request
    {
        #region Constructor

        protected ZeroCommandRequest(string commandCode) : base(commandCode)
        {
        }

        #endregion


        #region Public Methods

        public void ValidateResponseMessage(Memory<byte> responseMessage)
        {
            ZeroCommandResponse.ValidateResponseMessage(this, responseMessage);
        }

        public static ZeroCommandRequest CreateNew(AveryWeighTronixDevice device)
        {
            return new ZeroCommandRequest(Commands.ZeroCommand);
        }

        #endregion


        #region Protected Methods

        protected override void BuildMessageDetail(ref StringBuilder messageBuilder)
        {
        }

        #endregion
    }
}
