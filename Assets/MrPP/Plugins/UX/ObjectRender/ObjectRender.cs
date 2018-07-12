using GDGeek;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP {
    public class ObjectRender : MonoBehaviour {


        public string type { get; set; }
        private Camera _camera;
        private Transform _follow;

        public Transform follow {
            set {
                _follow = value;
            }
        }
        private RenderTexture renderTexture_;
        public RenderTexture renderTexture {
            get {
                return renderTexture_;
            }
        }
        public void Update() {
            if (_follow != null && _follow.transform.hasChanged) {

                float angle = 0f;
                Vector3 axis;
                _follow.transform.rotation.ToAngleAxis(out angle, out axis);
                this.transform.rotation = Quaternion.AngleAxis(angle, -axis);
            }
        }
        public void setup(Transform target)
        {
            GameObject obj = new GameObject("RenderCamera");
            obj.transform.SetParent(this.transform);
            _camera = obj.AskComponent<Camera>();
            _camera.enabled = true;
        
            this.transform.localPosition = Vector3.zero;
            _camera.transform.localPosition = Vector3.zero + new Vector3(0, 0, -0.3f);
           // Debug.Log(rectTransform.rect.width);
            Vector2Int size = new Vector2Int(256, 256);// new Vector2Int(Mathf.Abs(Mathf.FloorToInt(rectTransform.rect.width)), Mathf.Abs(Mathf.FloorToInt(rectTransform.rect.height)));
       

            renderTexture_ = new RenderTexture(size.x, size.y, 16, RenderTextureFormat.ARGB32);
            _camera.targetTexture = this.renderTexture_;
            _camera.clearFlags = CameraClearFlags.SolidColor;
            _camera.backgroundColor = Color.clear;
            _camera.nearClipPlane = 0.1f;
            _camera.farClipPlane = 0.5f;

            _camera.fieldOfView = 35f;

            this.gameObject.layer = target.gameObject.layer;
            _camera.gameObject.layer = target.gameObject.layer;
            _camera.cullingMask = LayerMask.GetMask(LayerMask.LayerToName(target.gameObject.layer));

      
        }

        internal void close()
        {


            this.gameObject.SetActive(false);
            //throw new NotImplementedException();
        }

        internal void open()
        {
            this.gameObject.SetActive(true);
            //throw new NotImplementedException();
        }

        public void OnDestroy()
        {
            Destroy(renderTexture_);
        }
    }
}