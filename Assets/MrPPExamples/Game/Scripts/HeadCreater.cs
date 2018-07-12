using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace MrPP {
    public class HeadCreater : NetworkBehaviour, INetworkCreater {
        public class Parameter : INetworkCreaterParameter
        {
            private int id_ = 0;
            public Parameter(int id) {
                id_ = id;
            }
            public string type
            {
                get
                {
                    return "Head";
                }
            }

            public IMessageWriter writer {
                get {
                    MessageWriter writer = new MessageWriter();
                    writer.onWriteTo += delegate (BinaryWriter bw)
                    {
                        bw.Write(id_);
                    };
                    return writer;

                }

            }
        };


        [SerializeField]
        [SyncVar(hook = "dataChange")]
        private int _id = 0;
        void dataChange(int id)
        {
            _id = id;
            Debug.Log(_id);
            refresh();
        }

        public IMessageReader reader
        {
            get
            {
                MessageReader reader = new MessageReader();
                reader.onReadFrom += delegate (BinaryReader br)
                {
                    _id = br.ReadInt32();
                };
                return reader;
            }
        }

        // Use this for initialization
        void Start() {
            refresh();
        }

        void refresh() {
           /* if (self)
            {
                //desplay
            }

            this.follow = hero;*/

        }
    }
}