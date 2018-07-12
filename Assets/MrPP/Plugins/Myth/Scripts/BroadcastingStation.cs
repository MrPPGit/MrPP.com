using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MrPP.Myth
{
    public class BroadcastingStation : NetworkBehaviour
    {
        public void broadcasting(IMessageHandler handler)
        {
            GameObject obj = handler.gameObject;
            IMessageHandler[] list = obj.GetComponents<IMessageHandler>();
            int n = 0;
            for (int i = 0; i < list.Length; ++i)
            {
                if (list[i] == handler)
                {
                    n = i;
                    break;
                }
            }
            IMessageWriter writer = handler.getWriter();
            Debug.Log(handler.gameObject.name);
            byte[] bytes = writer.writeTo();
            Debug.Log(bytes.Length);
            CmdSynchro(obj, bytes, n);
        }


        [Command]
        public void CmdSynchro(GameObject obj, byte[] bytes, int n)
        {
            if (obj == null)
            {
                return;
            }
            IMessageHandler[] list = obj.GetComponents<IMessageHandler>();
            if (list.Length > n && list[n] != null)
            {
                IMessageHandler handler = list[n];
                IMessageReader reader = handler.getRreader();
              //  Debug.Log(bytes.Length);
                //Debug.Log(handler.gameObject.name);
                reader.readFrom(bytes);
            }
        }

    }
}