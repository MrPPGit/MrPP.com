/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using UnityEngine.Events;

#if !NO_UNITY_VUFORIA


using Vuforia;

namespace MrPP { 
    /// <summary>
    ///     A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class TrackableHandler : MonoBehaviour, ITrackableEventHandler
    {

        public UnityEvent OnTrackingFound;
        public UnityEvent OnTrackingLost;


        private TrackableBehaviour.Status status_ = TrackableBehaviour.Status.NO_POSE;
        public TrackableBehaviour.Status status {
            get {
                return status_;
            }
        }
        #region PRIVATE_MEMBER_VARIABLES

        protected TrackableBehaviour mTrackableBehaviour;
        //protected VuMarkBehaviour _vuMarkBehaviour;

        #endregion // PRIVATE_MEMBER_VARIABLES

        #region UNTIY_MONOBEHAVIOUR_METHODS

        protected virtual void Start()
        {

            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS

        #region PUBLIC_METHODS

        /// <summary>
        ///     Implementation of the ITrackableEventHandler function called when the
        ///     tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
            TrackableBehaviour.Status previousStatus,
            TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
                if (OnTrackingFound != null) { 
                    OnTrackingFound.Invoke();
                }
            }
            else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                     newStatus == TrackableBehaviour.Status.NO_POSE)
            {
                Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
                if (OnTrackingLost != null)
                {
                    OnTrackingLost.Invoke();
                }
            }
            else
            {
                // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
                // Vuforia is starting, but tracking has not been lost or found yet
                // Call OnTrackingLost() to hide the augmentations
               // OnTrackingLost();

                if (OnTrackingLost != null)
                {
                    OnTrackingLost.Invoke();
                }
            }
        }

        #endregion // PUBLIC_METHODS

       
      
    }
}
#endif