using System;
using System.Collections;
using System.Collections.Generic;
using GDGeek;
using UnityEngine;
namespace MrPP
{
    namespace UX
    {
        public class ImageEffect : MonoBehaviour, IEffect
        {


            [SerializeField]
            private SpriteRenderer _target;



            [SerializeField]
            private Sprite _disable;


            [SerializeField]
            private Sprite _normal;

            [SerializeField]
            private Sprite _focus;


            [SerializeField]
            private Sprite _down;

            private SpriteRenderer target
            {
                get
                {
                    if (_target == null) {
                        _target = this.gameObject.GetComponent<SpriteRenderer>();
                    }
                    return _target;
                   
                }
            }


            public void focusExit()
            {

                _target.sprite = _normal;
                //GDGeek.TweenScale.Begin(target, 0.05f, oldScale_);
            }

            public void focusEnter()
            {


                _target.sprite = _focus;
                // GDGeek.TweenScale.Begin(target, 0f, oldScale_ * _scale);
            }

      
            virtual public void Awake()
            {
               // oldScale_ = target.transform.localScale;
            }

            public void inputDown()
            {

                _target.sprite = _down;
                //   GDGeek.TweenScale.Begin(target, 0.05f, oldScale_);
            }

            public void inputUp()
            {

                _target.sprite = _normal;
                // GDGeek.TweenScale.Begin(target, 0f, oldScale_ * _scale);
            }
        }
    }
}
