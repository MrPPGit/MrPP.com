using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MrPP.Myth { 
    public class InfoBoardState : MonoBehaviour
    {


        [SerializeField]
        private Text _uiTitle;

        [SerializeField]
        private Text _uiButton;

        [SerializeField]
        private string _button;
        [SerializeField]
        private string _title;

        [SerializeField]
        private InfoBoard.State _state;

        public InfoBoard.State state {
            get {
                return _state;
            }
        }
        [SerializeField]
        public List<GameObject> _enableList;


        public void enable()
        {
            _uiTitle.text = _title;
            _uiButton.text = _button;
            foreach (GameObject obj in _enableList) {
                obj.SetActive(true);
            }

        }
    }
}