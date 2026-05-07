using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour
{
    public TransitionEffect effect;


    public void ChangeScene(int index)
    {
        StartCoroutine(LoadScene(index));
    }

    IEnumerator LoadScene(int index)
    {
        effect.FadeOut();

        yield return  new WaitForSeconds(1);

        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        async.completed += (AsyncOperation obj) => { effect.FadeIn(); };
    }
}
