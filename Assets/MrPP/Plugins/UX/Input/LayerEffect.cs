using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP
{
    namespace UX
    {
        public class LayerEffect : MonoBehaviour, IEffect
        {




            [SerializeField]
            private string _down;

            [SerializeField]
            private string _normal;

            [SerializeField]
            private string _focus;

            public void focusExit()
            {
                this.gameObject.layer = LayerMask.NameToLayer(_normal);
                //_target.sprite = _normal;
                //GDGeek.TweenScale.Begin(target, 0.05f, oldScale_);
            }

            public void focusEnter()
            {


                this.gameObject.layer = LayerMask.NameToLayer(_focus);
                //_target.sprite = _focus;
                // GDGeek.TweenScale.Begin(target, 0f, oldScale_ * _scale);
            }


            virtual public void Awake()
            {
                // oldScale_ = target.transform.localScale;
            }

            public void inputDown()
            {

                this.gameObject.layer = LayerMask.NameToLayer(_down);
                //_target.sprite = _down;
                //   GDGeek.TweenScale.Begin(target, 0.05f, oldScale_);
            }

            public void inputUp()
            {
                this.gameObject.layer = LayerMask.NameToLayer(_normal);

                //_target.sprite = _normal;
                // GDGeek.TweenScale.Begin(target, 0f, oldScale_ * _scale);
            }
        }
    }
}
