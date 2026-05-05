using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionEffect : Singleton<TransitionEffect>
{
    private CanvasGroup canvasGroup;

    protected override void Init()
    {
        base.Init();
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        DontDestroyOnLoad(this.gameObject);
    }


    public void FadeIn()
    {
        canvasGroup.DOFade(0, 1);
    }

    public void FadeOut()
    {
        canvasGroup.DOFade(1, 1);
    }
}
