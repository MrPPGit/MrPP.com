using GDGeek;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP { 
    public class Exploded : MonoBehaviour, IExecute {

        public Tween.Method _method = Tween.Method.Linear;
        /*[Serializable]
        public class SelectedPart
        {
            public Select select;
        }*/
        [Serializable]
        public class Part {
            public Transform origin;
            public List<Transform> targets;
           // public Select select;
        }

        internal void reset()
        {
            foreach (var part in _parts)
            {
                part.origin.position = part.targets[0].position;
                part.origin.localScale = part.targets[0].localScale;
                part.origin.rotation = part.targets[0].rotation;
            }
        }

        public GameObject _origin;
        public GameObject _selectList;
        public List<GameObject> _targets;
        public List<Part> _parts;
       

        private Select findSelect(string name) {
            Select[] selects = gameObject.GetComponentsInChildren<Select>(true);
            for (int n = 0; n < selects.Length; ++n)
            {
                if (selects[n].name == name)
                {
                    return selects[n];
                }
            }
            return null;
        }

        internal GameObject findEnd(string target)
        {
            for (int n = 0; n < _parts.Count; ++n)
            {
                if (_parts[n].origin.name == target)
                {
                    return _parts[n].targets[_parts[n].targets.Count -1].gameObject;
                }
            }
            return null;
        }

        internal GameObject findBein(string target)
        {
            for (int n = 0; n < _parts.Count; ++n)
            {
                if (_parts[n].origin.name == target)
                {
                    return _parts[n].origin.gameObject;
                }
            }
            return null;
        }

        public void fixUp()
        {
            List<Renderer> list = getList(_origin);// new List<Renderer>();
            Debug.Log(list.Count);
           
            _parts = new List<Part>();
            for (int i = 0; i < list.Count; ++i)
            {
                Part part = new Part();
                part.origin = list[i].transform;
                /*
                Select select = this.findSelect(part.origin.name);
                if (select != null) {
                    select.transform.position = part.origin.position;
                }*/
                _parts.Add(part);
            }


            for (int i = 0; i < _targets.Count; ++i) {

                List<Renderer> l = getList(_targets[i]);
                if(_parts.Count == l.Count){
                    for (int n = 0; n < _parts.Count; ++n) {
                        if (_parts[n].origin.name == l[n].name) {
                            if (_parts[n].targets == null)
                            {
                                _parts[n].targets = new List<Transform>();
                            }
                            _parts[n].targets.Add(l[n].transform);
                        }
                    }
                }
                Debug.Log(l.Count);
            }
            
        }

        internal Task closeSelected(string name)
        {
            Select select = this.findSelect(name);
            if (select != null)
            {
                select.close();
            }
          
            return new Task();
        }

        internal Task selected(string name)
        {

            Select select = this.findSelect(name);
            if (select != null)
            {
                return select.openTask();
               
            }
            
            return new Task();
        }

        private Task partTask(GameObject obj, Transform transform, float duration) {

            TweenTask tt = new TweenTask(delegate () {
                Debug.Log(transform.name);
                Tween tween = TweenTransform.Begin(obj, duration, transform);
                tween.method = _method;
                return tween;
            });
            return tt;
        }

        public Task backTask(float duration)
        {
            TaskSet ts = new TaskSet();
            for (int i = 0; i < this._parts.Count; ++i)
            {
                Part part = this._parts[i];
              
                TaskList tl = new TaskList();
                for (int n = part.targets.Count-2; n >= 0; --n)
                {
                    Transform tranform = part.targets[n];
                    tl.push(partTask(part.origin.gameObject, tranform, duration));
                }
                ts.push(tl);
               

            }
            return ts;
        }
        public Task backTask(string name, float duration)
        {
            for (int i = 0; i < this._parts.Count; ++i)
            {
                Part part = this._parts[i];
                if (part.origin.name == name)
                {
                    TaskList tl = new TaskList();
                    for (int n = part.targets.Count - 2; n >= 0; --n)
                    {
                        Transform tranform = part.targets[n];
                        tl.push(partTask(part.origin.gameObject, tranform, duration));
                    }

                    return tl;
                }

            }
            return new Task();
        }
        public Task goTask(float duration) {
            TaskSet ts = new TaskSet();
            for (int i = 0; i < this._parts.Count; ++i)
            {
                Part part = this._parts[i];
               
                TaskList tl = new TaskList();
                for (int n = 1; n < part.targets.Count; ++n)
                {
                    Transform tranform = part.targets[n];
                    tl.push(partTask(part.origin.gameObject, tranform, duration));
                }
                ts.push(tl);
               

            }
            return ts;
        }
        public Task goTask(string name, float duration) {
            for (int i = 0; i < this._parts.Count; ++i) {
                Part part = this._parts[i];
                if (part.origin.name == name) {
                    TaskList tl = new TaskList();
                    for (int n = 1; n < part.targets.Count; ++n) {
                        Transform tranform = part.targets[n];
                        tl.push(partTask(part.origin.gameObject, tranform, duration));
                    }

                    return tl;
                }

            }
            return new Task();
        }
        private List<Renderer> getList(GameObject obj)
        {
            List<Renderer> list = new List<Renderer>();

            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>(true);
            Renderer begin = obj.GetComponent<Renderer>();
            if (begin != null)
            {
                if(begin.GetComponent< ExplodedIgnore >() == null){
                    list.Add(begin);
                }
               
            }
            foreach (var renderer in renderers)
            {
                if(renderer != null)
                {
                    if (renderer.GetComponent<ExplodedIgnore>() == null)
                    {
                        list.Add(renderer);
                    }
                }
            }
            return list;
        }



       
	
	    // Update is called once per frame
	    public void execute() {
            fixUp();
	    }
    }
}