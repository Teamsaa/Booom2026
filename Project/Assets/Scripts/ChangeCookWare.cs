using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCookWare : MonoBehaviour
{
    [Header("场景一变换")]
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
    private bool isEnd01 = false;
    private bool isEnd02 = false;

    [Header("场景二变换")]
    [SerializeField] private Animator bowlChangeCookWare02;
    [SerializeField] private GameObject sr_salt;
    [SerializeField] private GameObject fr_sugar;
    [SerializeField] private GameObject fr_baijiu;
    [SerializeField] private GameObject bowlGameObject01;
    [SerializeField] private GameObject bowlGameObject02;
    [SerializeField] private GameObject yzfr;
    [SerializeField] private GameObject yzsr;
    [SerializeField] private Sprite bowlSprite;


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
        if (!isEnd01 && bowl1.ISBowl1 && bowl2.ISBowl2)
        {
            Debug.Log("现在开始播放变换做菜场景动画01！");
            isEnd01 = true;
            DotweenAnimation01();
        }

        if (!isEnd02 && sr_salt.activeSelf && fr_baijiu.activeSelf && fr_sugar.activeSelf)
        {
            Debug.Log("现在开始播放变换做菜场景动画02！");
            isEnd02 = true;
            DotweenAnimation02();
        }


        
    }


    private void DotweenAnimation01()
    {
        DOTween.Sequence()
            .Append(playteCanvas.DOFade(0, 0.3f).OnComplete(() => Destroy(plate)))
            .AppendInterval(0.3f)
            .Append(choppingBoardCanvas.DOFade(0, 0.3f).OnComplete(() => Destroy(choppingBoard)))
            .AppendInterval(0.3f)
            .Append(knifeeCanvas.DOFade(0, 0.3f).OnComplete(() => Destroy(knife)))
            .AppendCallback(() => bowlChangeAnimator.Play("BowlChange"))
            .AppendInterval(0.3f)
            .Append(saltCanvas.DOFade(1, 0.3f))
            .AppendCallback(() => saltImage.raycastTarget = true)
            .AppendInterval(0.3f)
            .Append(sugarCanvas.DOFade(1, 0.3f))
            .AppendCallback(() => sugarImage.raycastTarget = true)
            .AppendInterval(0.3f)
            .Append(baijiuCanvas.DOFade(1, 0.3f))
            .AppendCallback(() => baijiuImage.raycastTarget = true)
            .OnComplete(() => {    
                DOTween.KillAll();
            });
    }

    private void DotweenAnimation02()
    {
        DOTween.Sequence()
            .Append(saltCanvas.DOFade(0, 0.3f)).OnComplete(() => Destroy(salt))
            .AppendInterval(0.3f)
            .Append(sugarCanvas.DOFade(0, 0.3f)).OnComplete(() => Destroy(sugar))
            .AppendInterval(0.3f)
            .Append(baijiuCanvas.DOFade(0, 0.3f)).OnComplete(() => Destroy(baijiu))
            .AppendCallback(() => { Destroy(fr_baijiu); Destroy(sr_salt); Destroy(fr_sugar); })
            .AppendInterval(0.3f)
            .AppendCallback(() => bowlChangeCookWare02.Play("bowlChangeCookWare02"))
            .OnComplete(() => {
                var temp1 = bowlGameObject01.GetComponent<UnityEngine.UI.Image>();
                var temp2 = bowlGameObject02.GetComponent<UnityEngine.UI.Image>();
                temp1.sprite = bowlSprite;
                temp2.sprite = bowlSprite;
                yzsr.SetActive(true);
                yzfr.SetActive(true);
                DOTween.KillAll();
            });
    }


}
