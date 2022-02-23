using System;
using System.Text.RegularExpressions;

namespace RICADO.AveryWeighTronix.SMA
{
    internal class ClearTareWeightResponse : Response
    {
        #region Constants

        private const string MessageRegex = "^([ZOUEIT\u0020])([0-9])([GTNgn])([M\u0020]).([0-9\u0020\u003A\u002E\\-]{10})(.{3})$";

        #endregion


        #region Constructor

#if NETSTANDARD
        protected ClearTareWeightResponse(Request request, byte[] responseMessage) : base(request, responseMessage)
        {
        }
#else
        protected ClearTareWeightResponse(Request request, Memory<byte> responseMessage) : base(request, responseMessage)
        {
        }
#endif

        #endregion


        #region Public Methods

#if NETSTANDARD
        public static void ValidateResponseMessage(ClearTareWeightRequest request, byte[] responseMessage)
        {
            _ = new ClearTareWeightResponse(request, responseMessage);
        }
#else
        public static void ValidateResponseMessage(ClearTareWeightRequest request, Memory<byte> responseMessage)
        {
            _ = new ClearTareWeightResponse(request, responseMessage);
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
                throw new SMAException("The Clear Tare Weight Response Message Format was Invalid");
            }

            WeightType weightType = splitInformation[3].ConvertToWeightType();

            if(weightType != WeightType.Gross)
            {
                throw new SMAException("Failed to Clear the Tare Weight. The Device responded with a Non-Gross Weight");
            }
        }

        #endregion
    }
}
