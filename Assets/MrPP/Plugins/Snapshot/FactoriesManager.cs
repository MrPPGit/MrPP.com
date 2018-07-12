using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP
{
    namespace Snapshot
    {

        public class FactoriesManager : GDGeek.Singleton<FactoriesManager>
        {
         
            public Target[] _phototypes;

            public Target create(string type, Target.IParameter parameter) {
                Target target = find(type);
                if (target != null)
                {
                    return target.create(parameter);
                }
                return null;
            }
            public Target create(string type, string json)
            {
                Target target = find(type);
                if(target != null) {
                    return target.create(json);
                }
                return null;
            }
            
            internal string serialize(Target target)
            {
                if (target != null)
                {
                    return target.toJson(target);
                }
                return null;
            }
          
            private Target find(string type)
            {
                for (int i = 0; i < _phototypes.Length; ++i)
                {
                    if (_phototypes[i].type == type)
                    {
                        return _phototypes[i];
                    }
                }
                return null;
            }
            internal Target unserialize(string type, string json)
            {
                Target target = this.find(type);
                if (target != null) {
                    return target.create(json);
                }
                return null;
            }
            internal Lens.TargetCreateTask unserializeCreateTask(string type, string json, PhotoTransform photoTransform)
            {
                Target target = this.find(type);
                if (target != null)
                {
                    return target.createTask(json, photoTransform);
                }
                return null;
            }

        }
    }
}