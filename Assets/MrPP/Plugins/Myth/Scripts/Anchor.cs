using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDGeek;
using System;

namespace MrPP.Myth
{


    public class Anchor : Singleton<Anchor>, IExecute, IAnchor
    {
        [SerializeField]
        private GameObject _mesh;
        [SerializeField]
        private Transform _sleep;
        [SerializeField]
        private Transform _weakup;


        public void go()
        {
            
        }
        public void wait() {
            TweenTransform.Begin(_mesh, 0.5f, _weakup);
            
        }

        public void stop() {

            TweenTransform.Begin(_mesh, 0.5f, _sleep);
            
        }
        public void hide() {
            foreach (Renderer render in _renderers)
            {
                render.enabled = false;
            }

            foreach (Collider collider in _colliders)
            {
                collider.enabled = false;
            }
        }
        public void show() {

           
            foreach (Renderer render in _renderers)
            {
                render.enabled = true;
            }
            
            foreach (Collider collider in _colliders)
            {
                collider.enabled = true;
            }
        }
        [SerializeField]
        private Renderer[] _renderers = null;
        [SerializeField]
        private Collider[] _colliders = null;
        public void execute()
        {
            _renderers = this.gameObject.GetComponentsInChildren<Renderer>();
            _colliders = this.gameObject.GetComponentsInChildren<Collider>();
           
        }
    }
}