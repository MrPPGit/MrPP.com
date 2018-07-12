using GDGeek;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System;

namespace MrPP.Myth{

    public class AsgardTranform : NetworkBehaviour, IMessageHandler
    {
        [Serializable]
        public struct Data
        {
            [SerializeField]
            public Vector3 asgardPosition;
            [SerializeField]
            public Quaternion asgardRotation;
            [SerializeField]
            public Vector3 asgardScale;
        };
        [SerializeField]
        private Transform _transform = null;

        public Transform getTransform() {

            if (_transform != null) {
                return _transform;
            }
            return this.transform;
        }
       // public get
        [SerializeField]
        private bool _interpolation = true;


        //private Data backup_;
        [SerializeField]
        [SyncVar(hook = "dataChange")]
        private Data data_;

        public void Awake()
        {
            //WWW www;
            //www.bytes



           // data_.asgardScale = this.transform.localScale;
          //  data_.asgardPosition = this.transform.localPosition;
           // data_.asgardRotation = this.transform.localRotation;
        }

        public void Start()
        {
            if (Altar.AmIGod)
            {
                synchro();
                sweep();
            }
        }
      
        void dataChange(Data data)
        {
        
            data_ = data;
            if (!amIGod) {
                //Debug.Log("DataChange" + this.gameObject.name);
                if (_interpolation && Vector3.Distance(data_.asgardPosition, this.getTransform().localPosition) < 0.3f )
                {
                    TweenTransformLocalData.Begin(getTransform().gameObject, 0.1f, data_.asgardPosition, data_.asgardRotation, data_.asgardScale);

                }
                else {
                    this.getTransform().localScale = data_.asgardScale;
                    this.getTransform().localPosition = data_.asgardPosition;
                    this.getTransform().localRotation = data_.asgardRotation;
                }

            }
        }
       

        private Data data
        {
            get {
                return data_;
            }
        }

        private Data backup_;

        public bool dirty
        {
            get
            {

                float distance = Vector3.Distance(backup_.asgardPosition, this.getTransform().localPosition);
                float scale = Vector3.Distance(backup_.asgardScale, this.getTransform().localScale);
                float angle = Quaternion.Angle(backup_.asgardRotation, this.getTransform().localRotation);
                if (distance > 0.01f || scale > 0.01f || angle > 1f)
                {
                    return true;
                }
                return false;
            }

        }
        private bool amIGod {
            get {
                WhoIsGod wig = Altar.LocalComponent<WhoIsGod>();
                if (wig != null && !wig.isItMe()) {
                    return false;
                }
                return true;

            }
        }

        public IMessageWriter getWriter()
        {
            MessageWriter writer = new MessageWriter();
            writer.onWriteTo += delegate (BinaryWriter bw)
            {
               // Debug.Log("Writer" + this.gameObject.name + this.transform.localPosition.ToString());
                bw.Write(this.getTransform().localPosition);
                bw.Write(this.getTransform().localRotation);
                bw.Write(this.getTransform().localScale);
            };
            return writer;
        }

        public IMessageReader getRreader()
        {
            MessageReader reader = new MessageReader();
            reader.onReadFrom += delegate (BinaryReader br)
            {
                Data data = new Data();
                data.asgardPosition = br.ReadVector3();
                data.asgardRotation = br.ReadQuaternion();
                data.asgardScale = br.ReadVector3();
                data_ = data;
               // Debug.Log("Rreader" + this.gameObject.name + data.asgardPosition.ToString());
            };
            return reader;
        }
       
        void Update()
        {
            if (amIGod && this.dirty)
            {

                synchro();
                sweep();
            }
         
        }

        private void sweep()
        {
            backup_.asgardPosition = this.getTransform().localPosition;
            backup_.asgardRotation = this.getTransform().localRotation;
            backup_.asgardScale = this.getTransform().localScale;
        }

        private void synchro()
        {
            BroadcastingStation bs = Altar.LocalComponent<BroadcastingStation>();
            if (bs) {
                bs.broadcasting(this);
            }
        }
    }
}