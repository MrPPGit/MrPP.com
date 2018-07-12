using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Myth {
    public class InfoBoard : GDGeek.Singleton<InfoBoard>
    {
        public enum State {
            Editor,
            ScanAndAlone,
            Scan,
            Hide,
            Logo,

        };
        
        [SerializeField]
        private List<GameObject> _all;

        public State state {

            set {
                    refresh(value);
                }
        }
      

        private void refresh(State data)
        {
            closeAll();

            InfoBoardState[] states = this.gameObject.GetComponentsInChildren<InfoBoardState>();
            foreach (InfoBoardState state in states) {
                if (state.state == data) {
                    state.enable();
                }

            }
        }

        private void closeAll()
        {
            foreach (GameObject obj in _all) {
                obj.SetActive(false);

            }
        }
    }

}