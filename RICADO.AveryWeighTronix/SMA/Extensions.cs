using System;
using System.Linq;
using System.Text;

namespace RICADO.AveryWeighTronix.SMA
{
    internal static class Extensions
    {
        internal static string ToMessageString(this UnitOfMeasure units)
        {
            switch(units)
            {
                case UnitOfMeasure.None:
                default:
                    return "\u0020\u0020\u0020";

                case UnitOfMeasure.Custom:
                    return "ooo";

                case UnitOfMeasure.Percent:
                    return "%\u0020\u0020";

                case UnitOfMeasure.Pounds:
                    return "lb\u0020";

                case UnitOfMeasure.Ounces:
                    return "oz\u0020";

                case UnitOfMeasure.Kilograms:
                    return "kg\u0020";

                case UnitOfMeasure.Grams:
                    return "g\u0020\u0020";
            }
        }

        internal static UnitOfMeasure ConvertToUnitOfMeasure(this string @string)
        {
            if (@string == null)
            {
                throw new ArgumentNullException(nameof(@string));
            }

            if (@string.Length == 0)
            {
                throw new ArgumentException("The Unit of Measure String cannot be Empty", nameof(@string));
            }

            switch(@string)
            {
                case "\u0020\u0020\u0020":
                    return UnitOfMeasure.None;

                case "ooo":
                    return UnitOfMeasure.Custom;

                case "%\u0020\u0020":
                    return UnitOfMeasure.Percent;

                case "lb\u0020":
                    return UnitOfMeasure.Pounds;

                case "oz\u0020":
                    return UnitOfMeasure.Ounces;

                case "kg\u0020":
                    return UnitOfMeasure.Kilograms;

                case "g\u0020\u0020":
                    return UnitOfMeasure.Grams;
            }

            throw new SMAException("Unknown or Unsupported Unit of Measure '" + @string + "'");
        }

        internal static WeightType ConvertToWeightType(this string @string)
        {
            if(@string == null)
            {
                throw new ArgumentNullException(nameof(@string));
            }

            if(@string.Length == 0)
            {
                throw new ArgumentException("The Weight Type String cannot be Empty", nameof(@string));
            }

            switch(@string)
            {
                case "G":
                case "g":
                    return WeightType.Gross;

                case "N":
                case "n":
                    return WeightType.Net;

                case "T":
                    return WeightType.Tare;
            }

            throw new SMAException("Unknown or Unsupported Weight Type '" + @string + "'");
        }
    }
}
