using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.UX {  
    public class IndicatorCountown : MonoBehaviour, ICountown
    {
        [SerializeField]
        private Renderer _renderer;
        public void Start()
        {
            if (_renderer == null) {
                _renderer = this.gameObject.GetComponent<Renderer>();
            }
            this.close();
            
        }
        public void close()
        {
            _renderer.enabled = false;
        }

        public void open()
        {
            _renderer.enabled = true;
        }

        public void percent(float per)
        {
            _renderer.sharedMaterial.SetFloat("_Angle", per *360f);
        }
    }
}