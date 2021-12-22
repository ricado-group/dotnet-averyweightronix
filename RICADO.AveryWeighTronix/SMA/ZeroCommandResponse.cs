using System;
using System.Text.RegularExpressions;

namespace RICADO.AveryWeighTronix.SMA
{
    internal class ZeroCommandResponse : Response
    {
        #region Constants

        private const string MessageRegex = "^([ZOUEIT\u0020])([0-9])([GTNgn])([M\u0020]).([0-9\u0020\u003A\u002E\\-]{10})(.{3})$";

        #endregion


        #region Constructor

        protected ZeroCommandResponse(Request request, Memory<byte> responseMessage) : base(request, responseMessage)
        {
        }

        #endregion


        #region Public Methods

        public static void ValidateResponseMessage(ZeroCommandRequest request, Memory<byte> responseMessage)
        {
            _ = new ZeroCommandResponse(request, responseMessage);
        }

        #endregion


        #region Protected Methods

        protected override void UnpackMessageDetail(string messageDetail)
        {
            string[] splitInformation;

            if (Regex.IsMatch(messageDetail, MessageRegex))
            {
                splitInformation = Regex.Split(messageDetail, MessageRegex);
            }
            else
            {
                throw new SMAException("The Zero Command Response Message Format was Invalid");
            }

            if (splitInformation[1] == "E")
            {
                throw new SMAException("Failed to Execute the Zero Command. A Zero Error Occurred");
            }

            if (splitInformation[1] == "I")
            {
                throw new SMAException("Failed to Execute the Zero Command. An Initial-Zero Error is Active");
            }

            if (splitInformation[1] == "T")
            {
                throw new SMAException("Failed to Execute the Zero Command. A Tare Error is Active");
            }
        }

        #endregion
    }
}
