using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP { 
    public class UIRenderManager : GDGeek.Singleton<UIRenderManager> {

        [System.Serializable]
        public class Item
        {
            [SerializeField]
            public string type;
            [SerializeField]
            public ObjectRenderFactory factory;
        }
        [SerializeField]
        private List<Item> _items;
        private Dictionary<string, Stack<ObjectRender> > pool_ = new Dictionary<string, Stack<ObjectRender> >();
        private Dictionary<string, ObjectRenderFactory> dictionary_ = new Dictionary<string, ObjectRenderFactory>();
        private void Awake()
        {
            dictionary_.Clear();
            foreach (Item item in _items) {
                dictionary_.Add(item.type, item.factory);
            }
        }
        public ObjectRender create(string type) {

            ObjectRender objRender = null;


            Debug.Log("ContainsKey:" + type);
            if (pool_.ContainsKey(type)) {
                Debug.Log(pool_[type].Count);
                if (pool_[type].Count != 0) {
                    
                    objRender =  pool_[type].Pop();
                }

                Debug.Log(pool_[type].Count);
            }

            if (objRender == null && dictionary_.ContainsKey(type)) { 
                objRender =  dictionary_[type].create();
            }
            // objRender.size = 
            if (objRender != null) { 
                objRender.type = type;
                objRender.open();
            }
            //objRender.refresh();
            return objRender;
          //  return null;
        }

        public void destroy(ObjectRender objRender)
        {
            if (objRender != null) {
                string type = objRender.type;
                if (!pool_.ContainsKey(type)) {
                    pool_[type] = new Stack<ObjectRender>();
                }
                objRender.close();
                pool_[type].Push(objRender);
                Debug.Log("push:" + type);
            }
        }
    }
}