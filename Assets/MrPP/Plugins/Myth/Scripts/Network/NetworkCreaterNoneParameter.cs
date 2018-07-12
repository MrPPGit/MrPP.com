using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP {
    public class NetworkCreaterNoneParameter : INetworkCreaterParameter {

        private string type_;
        public NetworkCreaterNoneParameter(string type) {
            type_ = type;
        }

        string INetworkCreaterParameter.type
        {
            get
            {
                return type_;
            }
        }

        IMessageWriter INetworkCreaterParameter.writer
        {
            get
            {
                return null;
            }
        }
    }
}