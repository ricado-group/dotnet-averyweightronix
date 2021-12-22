using System;

namespace RICADO.AveryWeighTronix
{
    public enum UnitOfMeasure
    {
        None,
        Custom,
        Percent,
        Pounds,
        Ounces,
        Kilograms,
        Grams
    }

    public enum WeightType
    {
        Gross,
        Tare,
        Net
    }

    public enum ConnectionMethod
    {
        Ethernet
    }

    public enum ProtocolType
    {
        SMA
    }
}
