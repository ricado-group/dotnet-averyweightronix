using System;

namespace RICADO.AveryWeighTronix.Channels
{
    internal struct ReceiveMessageResult
    {
#if NETSTANDARD
        internal byte[] Message;
#else
        internal Memory<byte> Message;
#endif
        internal int Bytes;
        internal int Packets;
    }
}
