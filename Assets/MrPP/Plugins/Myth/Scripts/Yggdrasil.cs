using GDGeek;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Myth
{

    //[RequireComponent(typeof(TweenTransform))]
    public class Yggdrasil : GDGeek.Singleton<Yggdrasil>
    {

        public class AsgardPose {
            public AsgardPose(Transform transform)
            {
                asgardPosition = transform.localPosition;
                asgardRotation = transform.localRotation;
                asgardScale = transform.localScale;
            }

            public AsgardPose(Vector3 asgardPosition, 
                Quaternion asgardRotation, 
                Vector3 asgardScale)
            {
                this.asgardPosition = asgardPosition;
                this.asgardRotation = asgardRotation;
                this.asgardScale = asgardScale;
            }
            public Vector3 asgardPosition {
                get;
                private set;
            }
            public Quaternion asgardRotation
            {
                get;
                private set;
            }


            public Vector3 asgardScale
            {
                get;
                private set;
            }
        }
        private void Start()
        {
            hasChanged = false;
        }
        void LateUpdate()
        {
            if (this.hasChanged)
            {
                this.hasChanged = false;
            }

        }
        public class WorldPose
        {
            public WorldPose(Transform transform)
            {
                position = transform.position;
                rotation = transform.rotation;
                scale = transform.lossyScale;
            }
            public Vector3 position
            {
                get;
                private set;
            }
            public Quaternion rotation
            {
                get;
                private set;
            }


            public Vector3 scale
            {
                get;
                private set;
            }
        }
        private GameObject vernier_ = null;
        private GameObject vernier
        {
            get {
                if (vernier_ == null) {
                    vernier_ = new GameObject("Vernier");
                    vernier_.transform.SetParent(this.transform);
                    vernier_.transform.localPosition = Vector3.zero;
                    vernier_.transform.localRotation = Quaternion.identity;
                    vernier_.transform.localScale = Vector3.one;
                }
                return vernier_;
            }

        }

        public WorldPose getWorldPose(AsgardPose asgard) {


            GameObject v = vernier;
            v.transform.localPosition = asgard.asgardPosition;
            v.transform.localRotation = asgard.asgardRotation;
            v.transform.localScale = asgard.asgardScale;
            return new WorldPose(v.transform);
        }
        public AsgardPose getAsgardPose(WorldPose world) {

            GameObject v = vernier;
            v.transform.position = world.position;
            v.transform.rotation = world.rotation;
            v.transform.setGlobalScale(world.scale);
            return new AsgardPose(v.transform);
        }

        public struct Data
        {
            [SerializeField]
            public Vector3 position;
            [SerializeField]
            public Quaternion rotation;
            [SerializeField]
            public Vector3 scale;
        };
        Data backup_;
        public bool hasChanged
        {
            set
            {
                // Yggdrasil.AsgardPose pose = Yggdrasil.Instance.getAsgardPose(new Yggdrasil.WorldPose(this._transform));
                backup_.position = transform.position;
                backup_.rotation = transform.rotation;
                backup_.scale = transform.lossyScale;

            }
            get
            {

                float distance = Vector3.Distance(backup_.position, transform.position);
                float scale = Vector3.Distance(backup_.scale, transform.lossyScale);
                float angle = Quaternion.Angle(backup_.rotation, transform.rotation);
                if (distance > 0.01f || scale > 0.01f || angle > 1f)
                {
                    return true;
                }


                return false;
            }
        }
        /*
        private Vector3 position_ = Vector3.zero;
        private Quaternion rotation_ = Quaternion.identity;
        [SerializeField]
        private bool _interpolating = false;
        [SerializeField]
        private Transform _target = null;
        private TweenTransform tween_ = null;
        public void Awake()
        {
            if (_interpolating)
            {
                position_ = _target.position;
                rotation_ = _target.rotation;
                tween_ = gameObject.AskComponent<TweenTransform>();
                tween_.enabled = false;
            }
            else
            {
                this.transform.SetParent(_target);
            }
            this.transform.position = _target.position;
            this.transform.rotation = _target.rotation;
        }
        public void Update()
        {
            if (_interpolating)
            {
                if (tween_.enabled == false)
                {
                    float distance = Vector3.Distance(position_, _target.transform.position);
                    float angle = Quaternion.Angle(rotation_, _target.rotation);
                    Debug.Log(distance);
                    if (distance > 0.005f || angle > 0.3f)
                    {
                        GDGeek.TweenTransform.Begin(this.gameObject, 0.05f, _target);
                    }
                }
            }
        }*/
    }
}