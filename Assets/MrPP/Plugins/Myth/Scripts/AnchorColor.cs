using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Myth
{


    public class AnchorColor : MonoBehaviour, IAnchor
    {
        [SerializeField]
        private Renderer _renderer;
        [SerializeField]
        private Color _stop;
        [SerializeField]
        private Color _wait;

        [SerializeField]
        private Color _go;

        public void go()
        {

            _renderer.sharedMaterial.color = _go;
            //_renderer.
        }
        public void wait()
        {
            _renderer.sharedMaterial.color = _wait;

        }

        public void stop()
        {

            _renderer.sharedMaterial.color = _stop;

        }
        public void hide()
        {
            _renderer.gameObject.SetActive(false);
        }
        public void show()
        {



            _renderer.gameObject.SetActive(true);
        }
    }
}