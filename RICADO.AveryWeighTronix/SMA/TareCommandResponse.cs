using System;
using System.Text.RegularExpressions;

namespace RICADO.AveryWeighTronix.SMA
{
    internal class TareCommandResponse : Response
    {
        #region Constants

        private const string MessageRegex = "^([ZOUEIT\u0020])([0-9])([GTNgn])([M\u0020]).([0-9\u0020\u003A\u002E\\-]{10})(.{3})$";

        #endregion


        #region Constructor

#if NETSTANDARD
        protected TareCommandResponse(Request request, byte[] responseMessage) : base(request, responseMessage)
        {
        }
#else
        protected TareCommandResponse(Request request, Memory<byte> responseMessage) : base(request, responseMessage)
        {
        }
#endif

        #endregion


        #region Public Methods

#if NETSTANDARD
        public static void ValidateResponseMessage(TareCommandRequest request, byte[] responseMessage)
        {
            _ = new TareCommandResponse(request, responseMessage);
        }
#else
        public static void ValidateResponseMessage(TareCommandRequest request, Memory<byte> responseMessage)
        {
            _ = new TareCommandResponse(request, responseMessage);
        }
#endif

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
                throw new SMAException("The Tare Command Response Message Format was Invalid");
            }

            if(splitInformation[1] == "E")
            {
                throw new SMAException("Failed to Execute the Tare Command. A Zero Error is Active");
            }

            if (splitInformation[1] == "I")
            {
                throw new SMAException("Failed to Execute the Tare Command. An Initial-Zero Error is Active");
            }

            if (splitInformation[1] == "T")
            {
                throw new SMAException("Failed to Execute the Tare Command. A Tare Error Occurred");
            }
        }

        #endregion
    }
}
