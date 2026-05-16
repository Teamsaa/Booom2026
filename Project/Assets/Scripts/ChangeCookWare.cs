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
    [SerializeField] private GameObject yzr01;
    [SerializeField] private GameObject yzr02;
    [SerializeField] private GameObject choppingBoard02;
    [SerializeField] private GameObject knife02;
    [SerializeField] private Sprite bowlSprite;
    [SerializeField] private Sprite yzfrSprite;
    [SerializeField] private Sprite yzsrSprite;
    


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
        var temp1 = bowlGameObject01.GetComponent<CanvasGroup>();
        var temp2 = bowlGameObject02.GetComponent<CanvasGroup>();
        var choppingBoard02Canvas = choppingBoard02.GetComponent<CanvasGroup>();
        var knife02Canvas = knife02.GetComponent<CanvasGroup>();
        var choppingBoard02Image = choppingBoard02.GetComponent<Image>();
        var knife02Image = knife02.GetComponent<Image>();
        DOTween.Sequence()
            .Append(saltCanvas.DOFade(0, 0.3f))
            .AppendCallback(() => Destroy(salt))
            .AppendInterval(0.3f)
            .Append(sugarCanvas.DOFade(0, 0.3f))
            .AppendCallback(() => Destroy(sugar))
            .AppendInterval(0.3f)
            .Append(baijiuCanvas.DOFade(0, 0.3f))
            .AppendCallback(() => Destroy(baijiu))
            .AppendCallback(() => { Destroy(fr_baijiu); Destroy(sr_salt); Destroy(fr_sugar); })
            .AppendInterval(0.3f)
            .AppendCallback(() => bowlChangeCookWare02.Play("bowlChangeCookWare02"))
            .AppendInterval(0.6f)
            .Append(temp1.DOFade(0, 0.5f))
            .Join(temp2.DOFade(0, 0.5f))
            .AppendCallback(() => {
                var temp1 = bowlGameObject01.GetComponent<UnityEngine.UI.Image>();
                var temp2 = bowlGameObject02.GetComponent<UnityEngine.UI.Image>();
                ChangeSprite(temp1, temp2);
                temp1.sprite = bowlSprite;
                temp2.sprite = bowlSprite;
            })
            .Append(temp1.DOFade(1, 0.5f))
            .Join(temp2.DOFade(1, 0.5f))
            .Append(choppingBoard02Canvas.DOFade(1, 0.5f))
            .Join(knife02Canvas.DOFade(1, 0.5f))
            .OnComplete(() => {
                choppingBoard02Image.raycastTarget = true;
                knife02Image.raycastTarget = true;
                yzr01.SetActive(true);
                yzr02.SetActive(true);
                DOTween.KillAll();
            });
    }


    private void ChangeSprite(Image bowl01, Image bowl02)
    {
        Debug.Log(bowl01.sprite.name + bowl02.sprite.name);
        var yzr01Sprite = yzr01.GetComponent<Image>();
        var yzr02Sprite = yzr02.GetComponent<Image>();
        Debug.Log($"{yzr01Sprite} + {yzr02Sprite}");
        if (bowl01.sprite.name == "zf_zwfr")
        {
            yzr01.name = "yzfr";
            yzr01Sprite.sprite = yzfrSprite;
        }
        else if (bowl01.sprite.name == "zf_zwsr")
        {
            yzr01.name = "yzsr";
            yzr01Sprite.sprite = yzsrSprite;
        }

        if (bowl02.sprite.name == "zf_zwfr")
        {
            yzr02.name = "yzfr";
            yzr02Sprite.sprite = yzfrSprite;
        }
        else if (bowl02.sprite.name == "zf_zwsr")
        {
            yzr02.name = "yzsr";
            yzr02Sprite.sprite = yzsrSprite;
        }
    }
}

