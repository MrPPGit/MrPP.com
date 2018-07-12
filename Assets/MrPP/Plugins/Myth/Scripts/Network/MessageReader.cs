using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace MrPP
{
    public class MessageReader : IMessageReader
    {
        public Action<BinaryReader> onReadFrom;
        public uint sender{set;get;}
        void IMessageReader.readFrom(byte[] bytes)
        {
            if (onReadFrom != null)
            {
                BinaryReader br = new BinaryReader(new MemoryStream(bytes));
               // sender = br.ReadUInt32();
                onReadFrom(br);
            }

        }


    }
}