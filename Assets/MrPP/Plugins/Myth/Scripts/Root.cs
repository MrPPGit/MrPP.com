using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Myth {
    public class Root : GDGeek.Singleton<Root> {
        [SerializeField]
        private Ctrl _ctrl;
      
        public Ctrl ctrl {
            get {

               
                return _ctrl;
            }
        }
        [SerializeField]
        private View _view;
        public View view
        {
            get
            {
                
                return _view;
            }
        }

        [SerializeField]
        private Model _model;
        public Model __model
        {
            get
            {

                return _model;
            }
        }
        public void Awake()
        {
            if (_ctrl == null)
            {
                _ctrl = this.gameObject.GetComponentInChildren<Ctrl>();
            }
            if (_view == null)
            {
                _view = this.gameObject.GetComponentInChildren<View>();
            }
            if (_model == null)
            {
                _model = this.gameObject.GetComponentInChildren<Model>();
            }

        }
    }
}