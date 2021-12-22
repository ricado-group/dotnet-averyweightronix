using System;

namespace RICADO.AveryWeighTronix.Channels
{
    internal struct ReceiveMessageResult
    {
        internal Memory<byte> Message;
        internal int Bytes;
        internal int Packets;
    }
}
