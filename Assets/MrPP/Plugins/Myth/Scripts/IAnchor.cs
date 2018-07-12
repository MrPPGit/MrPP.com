using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDGeek;

namespace MrPP.Myth
{
    public interface IAnchor 
    {

        GameObject gameObject { get; }
        void go();
        void wait();
        void stop();
        void hide();
        void show();
    }
}
