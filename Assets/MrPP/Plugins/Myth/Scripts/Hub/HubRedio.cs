using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MrPP.Myth;
namespace GDGeek
{
    
    [RequireComponent(typeof(MrPP.Myth.AsgardInt))]
    public class HubRedio : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _gameObjects;
        public GameObject[] gameObjects {
            set {
                _gameObjects = value;
            }
        }
        [SerializeField]
        private int? _now;

        [SerializeField]
        private int? _target;


        private bool _running = false;

        private List<IRedio> list_;

        private void Awake() {
            list_ = new List<IRedio>();
            foreach (GameObject obj in _gameObjects) {
                IRedio redio = obj.GetComponent<IRedio>();
                if (redio != null) {
                    list_.Add(redio);

                }

            }
            _now = -1;
            _target = -1;

        }
      
        public void updateTargets(AsgardInt aInt)
        {

            Debug.Log(aInt.data);
            if (_target == null)
            {
                _target = aInt.data;
                _now = aInt.data;
            }
            else {
                _target = aInt.data;
            }
          
        }
        private IRedio getRedio(int id)
        {

            foreach (IRedio redio in list_) {
                if (redio.id == id) {
                    return redio;
                }

            }
            return null;
        }
        public void Update()
        {
            refresh();
        }
        private void refresh()
        {

            if (_target != _now && !_running)
            {
                Task task = refreshTask();
                TaskManager.PushFront(task, delegate
                {
                    _running = true;

                });
                TaskManager.PushBack(task, delegate
                {
                    _running = false;

                });
                TaskManager.Run(task);
            }
        }
        private Task refreshTask()
        {
            TaskSet ts  = new TaskSet();
            if (_target.Value != _now.Value)
            {
            
                    IRedio now = getRedio(_now.Value);
                    if (now != null)
                    {
                        ts.push(now.close);
                    }


                    IRedio target = getRedio(_target.Value);

                    if (target != null)
                    {
                        ts.push(target.open);
                    }
               
            }
           

            TaskManager.PushFront(ts, delegate
            {
                _now = _target;
            });
            return ts;
        }
    }
    
}