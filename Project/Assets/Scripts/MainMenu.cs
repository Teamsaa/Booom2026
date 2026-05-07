using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindAnyObjectByType<SceneLoader>();
        sceneLoader.effect.FadeIn();
    }

    private void Update()
    {
        if (sceneLoader != null && Input.GetMouseButtonDown(0))
        {
            sceneLoader.ChangeScene(1);
        }
    }
}
