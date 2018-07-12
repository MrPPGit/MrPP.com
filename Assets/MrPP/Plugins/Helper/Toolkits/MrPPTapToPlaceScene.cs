// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
#if UNITY_WSA
using HoloToolkit.Unity.InputModule;
#endif
using UnityEngine;

namespace MrPP.Helper
{
    /// <summary>
    /// Lightweight game object placement
    /// </summary>
    public class MrPPTapToPlaceScene : MonoBehaviour
#if UNITY_WSA
        , IInputClickHandler
#endif
    { 
        public float DistanceFromHead = 1.0f;

        public bool Placing = false;

        Quaternion initialRotation;

        public void SetPlacing(bool placing)
        {
            this.Placing = placing;
        }

        private void OnEnable()
        {
            this.initialRotation = this.transform.rotation;
        }

        void Update()
        {
            if (Placing)
            {
                var cameraTransform = Camera.main.transform;
                var headPosition = cameraTransform.position;
                var forward = cameraTransform.forward;
                var scenePosition = headPosition + DistanceFromHead * forward;

                var facingRotation = cameraTransform.localRotation * this.initialRotation;
                //only yaw
                facingRotation.x = 0;
                facingRotation.z = 0;

                this.transform.position = scenePosition;
                this.transform.rotation = facingRotation;
            }

           // if (Input.GetMouseButtonDown(0))
          //  {
           //     Placing = false;
          //  }
        }
#if UNITY_WSA
        public void OnInputClicked(InputClickedEventData eventData)
        {
            Placing = !Placing;
        }
#else
        void OnMouseUpAsButton()
        {
            Placing = !Placing;
        }
#endif
    }
}