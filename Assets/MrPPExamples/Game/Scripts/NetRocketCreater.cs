using GDGeek;
using MrPP.Myth;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace MrPP.Example { 
    public class NetRocketCreater : NetworkBehaviour, INetworkCreater
    {
        [Serializable]
        public class Data {
            
            [SerializeField]
            public uint id;
        }


        public class Parameter : INetworkCreaterParameter
        {

            private Data data_;
            public Parameter(uint id) {
                data_ = new Data();
            
                data_.id = id;

            }
            public string type
            {
                get
                {
                    return "NetRocket";
                }
            }

            public IMessageWriter writer {
                get {
                    MessageWriter writer = new MessageWriter();
                    writer.onWriteTo += delegate (BinaryWriter bw)
                    {
                      
                        bw.Write(data_.id);
                    };
                    return writer;


                }
            }
        }

        [SerializeField]
        [SyncVar]
        private Data data_;
        public IMessageReader reader
        {
            get
            {
                MessageReader reader = new MessageReader();
                reader.onReadFrom += delegate (BinaryReader br)
                {
                    data_ = new Data();
                 
                    data_.id = br.ReadUInt32();

                };
                return reader;
            }
        }
        public void Start()
        {
            if (data_ != null) {
                Hero hero = HerosManager.Instance.getHeroByNetId(data_.id);
                this.transform.position = hero.transform.position;
                Rigidbody rigidbody = this.gameObject.GetComponent<Rigidbody>();
                this.transform.forward = hero.transform.forward;
                if (rigidbody != null)
                {
                    rigidbody.AddForce(hero.transform.forward * 800f);
                }


                if (Furnace.Instance.netId.Value == data_.id)
                {
                    this.gameObject.layer = LayerMask.NameToLayer("Body");
                }
                else {

                    this.gameObject.layer = LayerMask.NameToLayer("Enemy");
                }
            }


        }


    }
}