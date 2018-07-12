using System;
using System.Collections;
using System.Collections.Generic;
using GDGeek;
using UnityEngine;

namespace MrPP
{
    public class Select : MonoBehaviour
    {
      
        public Label _label;
        public Renderer _renderer;
        public SphereCollider _collider;
        // Use this for initialization
        void Start()
        {
            if (_label == null)
            {
                _label = this.gameObject.GetComponentInChildren<Label>();
            }
            close();
        }
        public bool isOpen{ get; set; }
        public void close() {
            _label.close();
            _renderer = this.gameObject.GetComponent<Renderer>();
            _renderer.enabled = true;
            _collider = this.gameObject.GetComponent<SphereCollider>();
            _collider.enabled = true;
            isOpen = false;
        }
        internal Task openTask()
        {
            Task task = _label.createTask();
            
            TaskManager.PushBack(task, delegate
            {
                Debug.Log("over");
                _renderer.enabled = true;
                _collider.enabled = true;
                isOpen = true;
            });
            return task;
        }
    }
}
