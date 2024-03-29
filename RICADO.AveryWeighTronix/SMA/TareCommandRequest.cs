﻿using System;
using System.Text;

namespace RICADO.AveryWeighTronix.SMA
{
    internal class TareCommandRequest : Request
    {
        #region Constructor

        protected TareCommandRequest(string commandCode) : base(commandCode)
        {
        }

        #endregion


        #region Public Methods

#if NETSTANDARD
        public void ValidateResponseMessage(byte[] responseMessage)
#else
        public void ValidateResponseMessage(Memory<byte> responseMessage)
#endif
        {
            TareCommandResponse.ValidateResponseMessage(this, responseMessage);
        }

        public static TareCommandRequest CreateNew(AveryWeighTronixDevice device)
        {
            return new TareCommandRequest(Commands.TareCommand);
        }

        #endregion


        #region Protected Methods

        protected override void BuildMessageDetail(ref StringBuilder messageBuilder)
        {
        }

        #endregion
    }
}
