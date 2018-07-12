using System;
using System.Collections;
using System.Collections.Generic;
using GDGeek;
using MrPP.Myth;
using MrPP.Platform;
using UnityEngine;
using UnityEngine.UI;

namespace MrPP { 
    public class DeviceItem : MonoBehaviour {

        public class Info {

        }
        public enum State {
            Joined,
            Ready,
        }


        [SerializeField]
        private RawImage _renderImage;
        [SerializeField]
        private Image _plane;
        [SerializeField]
        private Image _image;
        [SerializeField]
        private Button _button = null;
        [SerializeField]
        private State _state = State.Joined;

        [SerializeField]
        private Text _text;
        private int id_ = -1;
        public int id {
            get {
                return id_;
            }
        }

        public State state { get {

                return _state;
            }

        }

        bool selected_ = false;
        public bool selected {
            get {
                return selected_;
            }
        }
        public void select()
        {
            selected_ = true;

        }
        public void unselect() {
            selected_ = false;
        }
        ObjectRender _objRender = null;
        public void OnDestroy()
        {
            if (_objRender != null && UIRenderManager.IsInitialized) { 
                UIRenderManager.Instance.destroy(_objRender);
            }
        }
        internal void setup(Transform follow,  Hero.Data data, Hero.State state)
        {


            _text.text = data.ip + "\n" + data.name;
            id_ = data.id;
            if (state == Hero.State.Joined)
            {
                this._state = State.Joined;
            }
            else if (state == Hero.State.Ready) {
                this._state = State.Ready;
            }
            refresh();

            if (_objRender != null)
            {
                UIRenderManager.Instance.destroy(_objRender);
                _objRender = null;
            }
            _objRender = UIRenderManager.Instance.create(data.platform.ToString());
            _objRender.follow = follow;
            _renderImage.texture = _objRender.renderTexture;
            //_renderImage.rectTransform.sizeDelta 
            _renderImage.SetNativeSize();
       
        }

        public void setup(DeviceInfo info)
        {

            _text.text = info.ip + "\n" + info.title;
            id_ = info.id;
            this.name = "UI@" + info.title;

            if (_objRender != null)
            {
                UIRenderManager.Instance.destroy(_objRender);
                _objRender = null;
            }

            _objRender = UIRenderManager.Instance.create(info.platform.ToString());
            Debug.Log(_objRender);
            Debug.Log(info);
            _objRender.follow = info.follow;
            _renderImage.texture = _objRender.renderTexture;
            //_renderImage.SetNativeSize();
            _renderImage.rectTransform.sizeDelta = new Vector2(160f, 160f);
            _renderImage.rectTransform.localPosition = Vector3.zero;


            switch (info.state)
            {
                case DeviceInfo.State.Joined:
                    _button.enabled = false;
                    _plane.color = _joinedColor;
                    _renderImage.gameObject.hide();
                    _image.gameObject.show();
                    break;
                case DeviceInfo.State.Ready:
                    _renderImage.gameObject.show();

                    _image.gameObject.hide();
                    if (info.selected)
                    {
                        _button.enabled = false;
                        _plane.color = _selectedColor;
                    }
                    else
                    {
                        _button.enabled = true;
                        _plane.color = _readyColor;
                    }

                    break;

            }
        }

        [SerializeField]
        private Color _joinedColor;
        [SerializeField]
        private Color _readyColor;
        [SerializeField]
        private Color _selectedColor;

        public void refresh() {
            switch (this._state)
            {
                case State.Joined:
                    _button.enabled = false;
                    _plane.color = _joinedColor;
                    _renderImage.gameObject.hide();
                    _image.gameObject.show();
                    break;
                case State.Ready:
                    _renderImage.gameObject.show();

                    _image.gameObject.hide();
                    if (this.selected_)
                    {
                        _button.enabled = false;
                        _plane.color = _selectedColor;
                    }
                    else {
                        _button.enabled = true;
                        _plane.color = _readyColor;
                    }
                  
                    break;
                   
            }
        }
    }
}