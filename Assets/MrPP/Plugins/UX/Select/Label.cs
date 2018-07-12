using GDGeek;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrPP
{
    public class Label : MonoBehaviour
    {
        public LabelPoint _first;
        public LabelPoint _second;
        public LabelPoint _third;
        public GameObject _title;
        public GameObject _text;
        public Task createTask() {
            TaskList tl = new TaskList();
            tl.push(_first.createTask());
            TaskSet ts = new TaskSet();
            ts.push(_second.createTask());
            ts.push(_third.createTask());
            tl.push(ts);
            TaskManager.PushBack(tl, delegate
            {

                _title.SetActive(true);
                _text.SetActive(true);
            });
            return tl;
        }

        internal void close()
        {
            _first.init();
            _second.init();
            _third.init();
            _title.SetActive(false);
            _text.SetActive(false);
        }
    }
}