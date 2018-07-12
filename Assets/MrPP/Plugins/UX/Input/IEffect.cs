using GDGeek;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP { 
   
    public interface IEffect
    {
        void focusEnter();
        void focusExit();
        void inputDown();
        void inputUp();
    }
}