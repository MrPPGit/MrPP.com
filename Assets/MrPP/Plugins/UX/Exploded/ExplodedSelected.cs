using GDGeek;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrPP
{
    public class ExplodedSelected : MonoBehaviour, IExecute
    {
        public Exploded _exploded = null;
        public GameObject _selectList;

     
        private Select findSelect(string name)
        {
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

        public void fixUp()
        {
           // _exploded.fixUp();
            Select[] selects = gameObject.GetComponentsInChildren<Select>(true);
            foreach (var select in selects) {
                SelectPosition sp = select.gameObject.AskComponent<SelectPosition>();
                GameObject obj = null;
                if (sp._position == SelectPosition.Position.Begin)
                {
                    obj = _exploded.findBein(sp._target);
                }
                else {
                    obj = _exploded.findEnd(sp._target);
                }
                if (obj != null) {
                    sp.transform.position = obj.transform.position;
                }
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
        public void execute()
        {
            fixUp();
        }
    }
}
