using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP { 
    public interface IMessageReader
    {
        void readFrom(byte[] bytes);
    }
}