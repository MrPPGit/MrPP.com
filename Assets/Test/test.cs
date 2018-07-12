using System.Collections;
using System.Collections.Generic;
using MrPP;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour {
    [SerializeField]
    private ObjectRenderFactory _factory;
    [SerializeField]
    private Transform _follow;
    public void Start()
    {
        ObjectRender or =_factory.create();
        or.follow = this._follow;
        this.GetComponent<RawImage>().texture = or.renderTexture;
    }
}
