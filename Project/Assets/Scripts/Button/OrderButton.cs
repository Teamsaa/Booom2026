using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderButton : MonoBehaviour
{
    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindAnyObjectByType<SceneLoader>();
    }

    public void SceneLoader()
    {
        if (sceneLoader != null)
        {
            sceneLoader.ChangeScene(2);
        }
    }
}

