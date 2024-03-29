﻿using System;
using System.Text;

namespace RICADO.AveryWeighTronix.SMA
{
    internal class ClearTareWeightRequest : Request
    {
        #region Constructor

        protected ClearTareWeightRequest(string commandCode) : base(commandCode)
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
            ClearTareWeightResponse.ValidateResponseMessage(this, responseMessage);
        }

        public static ClearTareWeightRequest CreateNew(AveryWeighTronixDevice device)
        {
            return new ClearTareWeightRequest(Commands.ClearTareWeight);
        }

        #endregion


        #region Protected Methods

        protected override void BuildMessageDetail(ref StringBuilder messageBuilder)
        {
        }

        #endregion
    }
}
