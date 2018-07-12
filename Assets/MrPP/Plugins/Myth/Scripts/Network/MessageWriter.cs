using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace MrPP { 
    public class MessageWriter : IMessageWriter {


        public Action<BinaryWriter> onWriteTo;
        public byte[] writeTo()
        {
            if (onWriteTo != null) {

                BinaryWriter bw = new BinaryWriter(new MemoryStream());
              
                onWriteTo(bw);
                var stream = bw.BaseStream;


                stream.Position = 0;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;

               
            }
            return null;
        }

       
    }
}