using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Platform { 
    public class DeviceInfoList : MonoBehaviour, IEnumerable
    {

        private List<DeviceInfo> datas_ = null;

      

        #region IEnumerable implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public List<DeviceInfo>.Enumerator GetEnumerator()
        {
            DeviceInfo[] infos = this.gameObject.GetComponentsInChildren<DeviceInfo>();
            datas_ = new List<DeviceInfo>(infos);
            return datas_.GetEnumerator();
        }


        #endregion

      

        public void clear()
        {
            DeviceInfo[] infos = this.gameObject.GetComponentsInChildren<DeviceInfo>();

            foreach (DeviceInfo info in infos) {
                DestroyImmediate(info.gameObject);
            }
        }

        public DeviceInfo create()
        {
            GameObject go = new GameObject("DeviceInfo");
            go.transform.SetParent(this.transform);
            DeviceInfo info = go.AddComponent<DeviceInfo>();
            return info;
        }
    }
}