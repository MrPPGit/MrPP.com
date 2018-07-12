using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MrPP.Myth { 
    public class Pray : NetworkBehaviour
    {
        [Command]
        public void CmdCall() {

        }
        public static void Call(INetworkCallParameter parameter)
        {

            /*
            if (Furnace.Instance == null)
            {
                Prestrain_.Add(parameter);
            }
            else
            {
                Debug.Log(Furnace.Instance);
                byte[] bytes = null;
                if (parameter != null)
                {
                    IMessageWriter writer = parameter.writer;
                    if (writer != null)
                    {

                        bytes = writer.writeTo();
                    }
                }

                Furnace.Instance.CmdCreate(parameter.type, bytes);
            }

    */
        }
    }
}