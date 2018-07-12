using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP
{
    namespace Snapshot
    {
        public abstract class Target : MonoBehaviour
        {
          
            public interface IParameter
            {
                string toJson();
            }

            public PhotoTransform photoTrans;

            // public Target _phototype;
            public abstract string type { get; }
            public abstract Target create(string json);
            public abstract Target create(IParameter parameter);
            public abstract string toJson(Target obj);
            

            public bool _loaded = false;

            internal abstract Lens.TargetCreateTask createTask(string json, PhotoTransform photoTransform);
        }



    }
}
