using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP { 
    public class SelectPosition : MonoBehaviour {

        public enum Position
        {
            Begin,
            End,
        }
        public string _target;
        public Position _position = Position.Begin;
    }
}