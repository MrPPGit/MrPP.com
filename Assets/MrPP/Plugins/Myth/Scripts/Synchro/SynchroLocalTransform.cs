using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MrPP.Myth { 
    public class SynchroLocalTransform : NetworkBehaviour
    {
        // <summary>
        /// Sets the localPosition and localRotation on clients.
        /// </summary>
        /// <param name="postion">the localPosition to set</param>
        /// <param name="rotation">the localRotation to set</param>
        [Command(channel = 1)]
        public void CmdTransform(Vector3 position, Quaternion rotation)
        {
            //MrPP.HoloDebug.Log("CmdTransform:" + asgardPosition.ToString(), Color.green);
            localPosition_ = position;
            localRotation_ = rotation;
        }

        [SyncVar]
        private Vector3 localPosition_;

        public Vector3 localPosition
        {

            get
            {
                return localPosition_;
            }
        }
        [SyncVar]
        private Quaternion localRotation_;
        public Quaternion localRotation
        {

            get
            {
                return localRotation_;
            }
        }

        private void LateUpdate()
        {
           
            if (isLocalPlayer)
            {
                CmdTransform(this.transform.localPosition, this.transform.localRotation);
            }
            else
            {
                this.transform.localRotation = this.localRotation_;
                this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, this.localPosition_, 0.3f);                                                                                                      
          
            }
         
        }
    }
}