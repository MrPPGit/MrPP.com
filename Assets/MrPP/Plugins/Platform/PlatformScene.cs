using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MrPP { 
    public class PlatformScene : MonoBehaviour {
       // public Scene _scene;
        private void Awake()
        {
           
            bool isLoaded = false;
            PlatformInfo.Type type = PlatformInfo.Instance.type;
            Debug.Log(SceneManager.sceneCount);
           
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == type.ToString())
                {
                    isLoaded = true;
                }

            }
            if (!isLoaded)
            {
               SceneManager.LoadScene(type.ToString(), LoadSceneMode.Additive);
            }


        }
    }
}