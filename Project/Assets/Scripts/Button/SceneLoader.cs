using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour
{
    private TransitionEffect effect;

    private void Start()
    {
        effect = FindObjectOfType<TransitionEffect>();
    }

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
