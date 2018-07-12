using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP
{
    public interface IMessageWriter
    {
        byte[] writeTo();
    }
}