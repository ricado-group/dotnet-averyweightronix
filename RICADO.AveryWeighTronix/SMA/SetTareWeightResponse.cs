using System;
using System.Text.RegularExpressions;

namespace RICADO.AveryWeighTronix.SMA
{
    internal class SetTareWeightResponse : Response
    {
        #region Constants

        private const string MessageRegex = "^([ZOUEIT\u0020])([0-9])([GTNgn])([M\u0020]).([0-9\u0020\u003A\u002E\\-]{10})(.{3})$";

        #endregion


        #region Constructor

#if NETSTANDARD
        protected SetTareWeightResponse(Request request, byte[] responseMessage) : base(request, responseMessage)
        {
        }
#else
        protected SetTareWeightResponse(Request request, Memory<byte> responseMessage) : base(request, responseMessage)
        {
        }
#endif

        #endregion


        #region Public Methods

#if NETSTANDARD
        public static void ValidateResponseMessage(SetTareWeightRequest request, byte[] responseMessage)
        {
            _ = new SetTareWeightResponse(request, responseMessage);
        }
#else
        public static void ValidateResponseMessage(SetTareWeightRequest request, Memory<byte> responseMessage)
        {
            _ = new SetTareWeightResponse(request, responseMessage);
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
                throw new SMAException("The Set Tare Weight Response Message Format was Invalid");
            }

            if (splitInformation[1] == "E")
            {
                throw new SMAException("Failed to Set the Tare Weight. A Zero Error is Active");
            }

            if (splitInformation[1] == "I")
            {
                throw new SMAException("Failed to Set the Tare Weight. An Initial-Zero Error is Active");
            }

            if (splitInformation[1] == "T")
            {
                throw new SMAException("Failed to Set the Tare Weight. A Tare Error Occurred");
            }

            WeightType weightType = splitInformation[3].ConvertToWeightType();

            if(weightType != WeightType.Net)
            {
                throw new SMAException("Failed to Set the Tare Weight. The Device is not indicating a Net Weight");
            }
        }

        #endregion
    }
}
