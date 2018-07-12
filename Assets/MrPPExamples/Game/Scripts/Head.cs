using MrPP.Myth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP { 
    public class Head : MonoBehaviour {


        public void refresh(MrPP.Myth.Hero hero) {
            Debug.Log(hero.data.platform);
        }
    }
}