using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Platform { 
    public class UITest : MonoBehaviour {


        [SerializeField]
        private bool _selected = false;
        [SerializeField]
        private bool _loading = false;
        [SerializeField]
        private bool _marking = false;
        [SerializeField]
        private bool _running = false;

        [SerializeField]
        private bool _barRefresh = false;



        [SerializeField]
        private bool _showBar = false;


        [SerializeField]
        private bool _hideBar = false;
        // private bool _hide = 
        // Use this for initialization
        void Start () {
		
	    }
	
	    // Update is called once per frame
	    void Update () {
            if (_loading) {
                HudManager.Instance.loading();
                 _loading = false;
            }
            if (_selected)
            {
                HudManager.Instance.selecting();
                _selected = false;
            }

            if (_marking)
            {
                HudManager.Instance.marking();
                _marking = false;
            }

            if (_running)
            {
                HudManager.Instance.running();
                _running = false;
            }
            if (_barRefresh)
            {
                HudManager.Instance.barRefresh();
                _barRefresh = false;
            }
            if (_showBar) {
                HudManager.Instance.showBar();
                _showBar = false;
            }


            if (_hideBar)
            {
                HudManager.Instance.hideBar();
                _hideBar = false;
            }
        }


    }
}
