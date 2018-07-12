using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace MrPP.Myth
{
    public class AsgardInt : NetworkBehaviour, IMessageHandler
    {


        [SerializeField]
        [SyncVar(hook = "dataChange")]
        private int _data;

        public int data
        {
            get
            {
                return _data;
            }
        }




        public override void OnStartClient()
        {
            base.OnStartClient();
            if (_onDataChange != null)
            {
                _onDataChange.Invoke();
            }

        }
        public UnityEvent _onDataChange;
        void dataChange(int data)
        {
            _data = data;
            if (_onDataChange != null)
            {
                _onDataChange.Invoke();
            }
        }

        private int value_ = 0;

        public void setValue(int value)
        {
            value_ = value;
            BroadcastingStation bs = Altar.LocalComponent<BroadcastingStation>();
            if (bs)
            {
                bs.broadcasting(this);
            }
        }

        public IMessageWriter getWriter()
        {
            MessageWriter writer = new MessageWriter();
            writer.onWriteTo += delegate (BinaryWriter bw)
            {
                bw.Write(value_);
            };
            return writer;
        }

        public IMessageReader getRreader()
        {
            MessageReader reader = new MessageReader();
            reader.onReadFrom += delegate (BinaryReader br)
            {
                _data = br.ReadInt32();
            };
            return reader;
        }
    }
}
