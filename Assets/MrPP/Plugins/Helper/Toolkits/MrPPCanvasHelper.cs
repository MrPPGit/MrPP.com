// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
#if  UNITY_WSA
using HoloToolkit.Unity.InputModule;
#endif
namespace MrPP.Helper
{
    /// <summary>
    /// Helper class for setting up canvases for use in the MRTK.
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class MrPPCanvasHelper : MonoBehaviour
    {
        /// <summary>
        /// The canvas this helper script is targeting.
        /// </summary>
        public Canvas Canvas;
#if UNITY_WSA
      

        private void Awake()
        {
            if (Canvas == null)
            {
                Canvas = GetComponent<Canvas>();
            }
        }

        private void Start()
        {
            if (!FocusManager.IsInitialized) {
                return;
            }
            FocusManager.AssertIsInitialized();
            Debug.Assert(Canvas != null);

            if (Canvas.isRootCanvas && Canvas.renderMode == RenderMode.WorldSpace)
            {
                Canvas.worldCamera = FocusManager.Instance.UIRaycastCamera;
            }
        }

#endif
    }
}
