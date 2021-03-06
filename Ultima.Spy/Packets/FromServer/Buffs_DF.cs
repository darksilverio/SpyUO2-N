﻿using System.Collections.ObjectModel;
using System.IO;
using System.Collections.Generic;
using System;

namespace Ultima.Spy.Packets
{
	[UltimaPacket( "Buffs", UltimaPacketDirection.FromServer, 0xDF )]
	public class BuffsPacket : UltimaPacket, IUltimaEntity
	{
        private uint _Serial;
        [UltimaPacketProperty("Serial", "0{0:X}")]
        public uint Serial { get { return _Serial; } }

		private BuffType _Type;
		[UltimaPacketProperty( "Type", "{0:D} - {0}" )]
		public BuffType Type { get { return _Type; } }

		private List<BuffItem> _BuffList;
		[UltimaPacketProperty( "Buffs" )]
		public List<BuffItem> BuffList { get { return _BuffList; } }

		protected override void Parse( BigEndianReader reader )
		{
			reader.ReadByte(); // ID
			reader.ReadInt16(); // Size

			_Serial = reader.ReadUInt32();

			int type = reader.ReadInt16();

			if ( type >= 0x3EA )
				_Type = BuffType.BuffIcons;
			else
				_Type = (BuffType) type;

			int count = reader.ReadInt16();
			_BuffList = new List<BuffItem>( count  );

			for ( int i = 0; i < count; i++ )
				_BuffList.Add( new BuffItem( reader ) );
		}
	}

	public class BuffItem
	{
		private BuffSourceType _SourceType;
		[UltimaPacketProperty( "Source Type", "{0:D} - {0}" )]
		public BuffSourceType SourceType { get { return _SourceType; } }

		private int _IconID;
		[UltimaPacketProperty( "Icon ID", "0{0:X}" )]
		public int IconID { get { return _IconID; } }

		private int _QueueIndex;
		[UltimaPacketProperty( "Queue Index" )]
		public int QueueIndex { get { return _QueueIndex; } }

		private int _Duration;
		[UltimaPacketProperty( "Duration (s)" )]
		public int Duration { get { return _Duration; } }

		private int _TitleCliloc;
		[UltimaPacketProperty( "Title Cliloc" )]
		public int TitleCliloc { get { return _TitleCliloc; } }

		private string _TitleArguments;
		public string TitleArgumentsList { get { return _TitleArguments; } }
		[UltimaPacketProperty( "Title Arguments" )]
		public string TitleArguments { get { return String.Join( "|", _TitleArguments ); }
		}

		private int _SecondaryCliloc;
		[UltimaPacketProperty( "Secondary Cliloc" )]
		public int SecondaryCliloc { get { return _SecondaryCliloc; } }

		private string _SecondaryArguments;
		public string SecondaryArgumentsList { get { return _SecondaryArguments; } }
		[UltimaPacketProperty( "Secondary Arguments" )]
		public string SecondaryArguments { get { return String.Join( "|", _SecondaryArguments ); } }

		private int _TernaryCliloc;
		[UltimaPacketProperty( "Ternary Cliloc" )]
        public int TernaryCliloc { get { return _TernaryCliloc; } }

		private string _TernaryArguments;
		public string TernaryArgumentsList { get { return _TernaryArguments; } }
		[UltimaPacketProperty( "Ternary Arguments" )]
		public string TernaryArguments { get { return String.Join( "|", _TernaryArguments ); } }

		public BuffItem( BigEndianReader reader )
		{
			_SourceType = (BuffSourceType) reader.ReadInt16();
			reader.ReadInt16(); // unknown
			_IconID = reader.ReadInt16();
			_QueueIndex = reader.ReadInt16();
			reader.ReadInt32(); // unknown
			_Duration = reader.ReadInt16();
			reader.ReadBytes( 3 ); // unknown
			_TitleCliloc = reader.ReadInt32();
			_SecondaryCliloc = reader.ReadInt32();
			_TernaryCliloc = reader.ReadInt32();
			_TitleArguments = reader.ReadUnicodeString();
			_SecondaryArguments = reader.ReadUnicodeString();
			_TernaryArguments = reader.ReadUnicodeString();
		}

		public override string ToString()
		{
			return String.Format( "{0} - {1}", _SourceType, _TitleCliloc );
		}
	}
}