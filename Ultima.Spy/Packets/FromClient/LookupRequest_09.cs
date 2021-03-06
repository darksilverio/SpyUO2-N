﻿using System.Collections.ObjectModel;
using System.IO;

namespace Ultima.Spy.Packets
{
    [UltimaPacket("Lookup Request", UltimaPacketDirection.FromClient, 0x09)]
    public class LookupRequestPacket : UltimaPacket
    {
        private uint _Serial;
        [UltimaPacketProperty("Serial", "0{0:X}")]
        public uint Serial { get { return _Serial; } }

        protected override void Parse(BigEndianReader reader)
        {
            reader.ReadByte(); // ID
            _Serial = reader.ReadUInt32();
        }
    }
}