using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrPP.Example {
    public class PlayerHandler : MonoBehaviour {
        [SerializeField]
        private NetPlayer _player;
        public NetPlayer player
        {
            get {
                return _player;
            }

        }
    }
}