using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using MrPP.Helper.InputModule;


namespace MrPP{
	namespace UX{

		[RequireComponent(typeof(MrPP.UX.Clicker)), RequireComponent(typeof(MrPP.UX.InputHandler))]
		public class ButtonFocusable : MonoBehaviour, IEffect
        {

            public void focusEnter() {
                start();
            }
            public void focusExit()
            {
                over();
            }
            public void inputDown()
            {
                over();
            }
            public void inputUp()
            {
                over();
            }

            private bool running_ = false;
			private float time_ = 0.0f;
			[SerializeField]
			private float _foucusTime = 2.0f;
			private void start(){
				running_ = true;
				time_ = 0.0f;
                if (countown_ != null) {
                    countown_.open();
                }

            }
			private void over(){
				running_ = false;
                time_ = 0.0f;


                if (countown_ != null)
                {
                    countown_.close();
                }
            }
			private void execute(){
                IEffect[] effects = this.gameObject.GetComponents<IEffect>();
                foreach (var e in effects)
                {
                    e.inputDown();
                }
                this.clicker_.execute ();
			}
			
			private MrPP.UX.Clicker clicker_ = null;
            private ICountown countown_ = null; 
           // private MrPP.UX.InputHandler input_ = null;
            // Use this for initialization
            void Start ()
            {

                countown_ = this.gameObject.GetComponentInChildren<ICountown>();
                clicker_ = this.gameObject.GetComponent<MrPP.UX.Clicker>();
                running_ = false;
			}
			
			// Update is called once per frame
			void Update () {
				if (this.running_) {
					time_ += Time.deltaTime;

                    if (time_ > this._foucusTime)
                    {


                        if (countown_ != null)
                        {
                            countown_.percent(1f);
                        }
                        this.over();
                        this.execute();
                    }
                    else {
                        if (countown_ != null)
                        {
                            countown_.percent(time_ / this._foucusTime);
                        }
                    }
				}
			}
		}
	}
}