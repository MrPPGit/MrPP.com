using GDGeek;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrPP
{
    public class LabelPoint : MonoBehaviour
    {
        public float _time = 0.1f;
        public float _length = 25f;
        public GameObject _point;
        public GameObject _line;
        public void init()
        {

            this._line.transform.localScale = new Vector3(1, 0, 1);
            _point.SetActive(false);
            _line.SetActive(false);
        }
        public Task createTask() {
            TaskSet ts = new TaskSet();
           
            ts.push(pointTask());
            ts.push(listTask());
            return ts;
        }

        private Task listTask()
        {
            TweenTask tt = new TweenTask(delegate () {

                _line.SetActive(true);
                Tween tween = TweenScale.Begin(this._line, _time, new Vector3(1, _length, 1));
                return tween;
            });
            TaskManager.PushFront(tt, delegate
            {
                this._line.transform.localScale = new Vector3(1, 0, 1);
            });
            return tt;
        }

        private Task pointTask()
        {
            float time = _time;
            //float flicker = 0.05f;
           // float all = 0f;
            TaskWait task = new TaskWait(time);
            TaskManager.PushFront(task, delegate
            {
              //  all = 0f;
                _point.SetActive(true);
            });

            TaskManager.PushBack(task, delegate
            {
                _point.SetActive(true);
            });
            /*
            TaskManager.AddUpdate(task, delegate (float d)
            {
                all += d;
                float fn = all / flicker;
                int n = Mathf.FloorToInt(fn);
                if ((n % 2) == 0)
                {
                    _point.SetActive(true);
                }
                else {
                    _point.SetActive(false);
                }
            });*/
            return task;
        }
    }

}