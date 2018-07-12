using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP { 
    public interface INetworkCreater  {
        IMessageReader reader {
            get;
        }

    }
}