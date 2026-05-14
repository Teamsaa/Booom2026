using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCookWare : MonoBehaviour
{
    [SerializeField] private Animator bowlChangeAnimator;
    [SerializeField] private GameObject choppingBoard;
    [SerializeField] private GameObject knife;
    [SerializeField] private GameObject plate;
    [SerializeField] private GameObject baijiu;
    [SerializeField] private GameObject salt;
    [SerializeField] private GameObject sugar;

    [SerializeField] private Bowl bowl1;
    [SerializeField] private Bowl bowl2;

    private CanvasGroup playteCanvas;
    private CanvasGroup choppingBoardCanvas;
    private CanvasGroup knifeeCanvas;
    private CanvasGroup baijiuCanvas;
    private CanvasGroup saltCanvas;
    private CanvasGroup sugarCanvas;

    private Image baijiuImage;
    private Image saltImage;
    private Image sugarImage;
    private bool isEnd = false;

    private void Start()
    {
        playteCanvas = plate.GetComponent<CanvasGroup>();
        choppingBoardCanvas = choppingBoard.GetComponent<CanvasGroup>();
        knifeeCanvas = knife.GetComponent<CanvasGroup>();
        baijiuCanvas = baijiu.GetComponent<CanvasGroup>();
        saltCanvas = salt.GetComponent<CanvasGroup>();
        sugarCanvas = sugar.GetComponent<CanvasGroup>();
        baijiuImage = baijiu.GetComponent<Image>();
        saltImage = salt.GetComponent<Image>();
        sugarImage = sugar.GetComponent<Image>();
    }

    private void Update()
    {
        if (bowl1.ISBowl1 && bowl2.ISBowl2 && !isEnd)
        {
            Debug.Log("现在开始播放变换做菜场景动画！");
            DotweenAnimation();
        }
    }


    private void DotweenAnimation()
    {
        DOTween.Sequence()
            .Append(playteCanvas.DOFade(0, 0.5f).OnComplete(() => Destroy(plate)))
            .AppendInterval(0.3f)
            .Append(choppingBoardCanvas.DOFade(0, 0.5f).OnComplete(() => Destroy(choppingBoard)))
            .AppendInterval(0.3f)
            .Append(knifeeCanvas.DOFade(0, 0.5f).OnComplete(() => Destroy(knife)))
            .AppendCallback(() => bowlChangeAnimator.Play("BowlChange"))
            .AppendInterval(0.3f)
            .Append(saltCanvas.DOFade(1, 0.5f))
            .AppendCallback(() => saltImage.raycastTarget = true)
            .AppendInterval(0.3f)
            .Append(sugarCanvas.DOFade(1, 0.5f))
            .AppendCallback(() => sugarImage.raycastTarget = true)
            .AppendInterval(0.3f)
            .Append(baijiuCanvas.DOFade(1, 0.5f))
            .AppendCallback(() => baijiuImage.raycastTarget = true)
            .OnComplete(() => {
                isEnd = true;
                Destroy(this.gameObject);
            });
    }

}
