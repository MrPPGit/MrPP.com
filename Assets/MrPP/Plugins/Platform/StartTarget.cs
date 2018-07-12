using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

namespace MrPP.Platform { 
    public class StartTarget : GDGeek.Singleton<StartTarget>, ITrackableEventHandler
    {


        [SerializeField]
        private Transform _transform;
        public interface IListener {
            void onFound(Transform transform);
            void onLost();

        }

        private HashSet<IListener> listeners_ = new HashSet<IListener>();

        public void addListener(IListener listener) {
            listeners_.Add(listener);

        }
        public void removeListener(IListener listener)
        {
            listeners_.Remove(listener);

        }
        private TrackableBehaviour.Status status_ = TrackableBehaviour.Status.NO_POSE;
        public TrackableBehaviour.Status status
        {
            get
            {
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

            if (_transform == null) {
                _transform = this.transform;
            }
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

                foreach (IListener listener in listeners_) {
                    listener.onFound(_transform);
                }
              
            }
            else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                     newStatus == TrackableBehaviour.Status.NO_POSE)
            {
                Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
                foreach (IListener listener in listeners_)
                {
                    listener.onLost();
                }
            }
            else
            {
                // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
                // Vuforia is starting, but tracking has not been lost or found yet
                // Call OnTrackingLost() to hide the augmentations
                // OnTrackingLost();

                foreach (IListener listener in listeners_)
                {
                    listener.onLost();
                }
            }
        }

        #endregion // PUBLIC_METHODS

    }
}