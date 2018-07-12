using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP { 
    public interface INetworkCreaterParameter{
        string type {
            get;
        }
        IMessageWriter writer {
            get;

        }
    }
}