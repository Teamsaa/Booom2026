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
    private bool isEnd03 = false;

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

    [Header("场景三变换")]
    [SerializeField] private GameObject fz;
    [SerializeField] private GameObject jdpz;
    [SerializeField] private GameObject jd;
    [SerializeField] private GameObject srw;
    [SerializeField] private GameObject qhsr;
    [SerializeField] private GameObject frw;
    [SerializeField] private GameObject qhfr;
    [SerializeField] private GameObject ycw;
    [SerializeField] private GameObject yc;
    [SerializeField] private GameObject csj;
    [SerializeField] private GameObject bowl01GameObject;
    [SerializeField] private GameObject bowl02GameObject;
    [SerializeField] private GameObject qhfrBefore;
    [SerializeField] private GameObject qhsrBefore;
    [SerializeField] private GameObject yzfrAnimator;
    [SerializeField] private GameObject yzsrAnimator;


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

        if (!isEnd03 && bowl1.ISFull && bowl2.ISFull)
        {
            Debug.Log("现在开始播放变换做菜场景动画03！");
            isEnd03 = true;
            DotweenAnimation03();
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

    private void DotweenAnimation03()
    {
        GameEvent.isScene3 = true;
        var fzImage = fz.GetComponent<Image>();
        var jdpzImage = jdpz.GetComponent<Image>();
        var jdImage = jd.GetComponent<Image>();
        var srwImage = srw.GetComponent<Image>();
        var qhsrImage = qhsr.GetComponent<Image>();
        var frwImage = frw.GetComponent<Image>();
        var qhfrImage = qhfr.GetComponent<Image>();
        var ycwImage = ycw.GetComponent<Image>();
        var ycImage = yc.GetComponent<Image>();
        var csjImage = csj.GetComponent<Image>();

        var fzCanvas = fz.GetComponent<CanvasGroup>();
        var jdpzCanvas = jdpz.GetComponent<CanvasGroup>();
        var srwCanvas = srw.GetComponent<CanvasGroup>();
        var frwCanvas = frw.GetComponent<CanvasGroup>();
        var ycwCanvas = ycw.GetComponent<CanvasGroup>();
        var csjCanvas = csj.GetComponent<CanvasGroup>();
        var qhfrBeforeCanvas = qhfrBefore.GetComponent<CanvasGroup>();
        var qhsrBeforeCanvas = qhsrBefore.GetComponent<CanvasGroup>();
        var knife02Canvas = knife02.GetComponent<CanvasGroup>();
        var bowl01Canvas = bowl01GameObject.GetComponent<CanvasGroup>();
        var bowl02Canvas = bowl02GameObject.GetComponent<CanvasGroup>();

        DOTween.Sequence()
            .Append(bowl01Canvas.DOFade(0, 0.3f))
            .Join(qhfrBeforeCanvas.DOFade(0, 0.3f))
            .Append(bowl02Canvas.DOFade(0, 0.3f))
            .Join(qhsrBeforeCanvas.DOFade(0, 0.3f))
            .Append(knife02Canvas.DOFade(0, 0.3f))
            .AppendCallback(() =>
            {
                Destroy(bowl01GameObject);
                Destroy(bowl02GameObject);
                Destroy(knife02);
                Destroy(qhfrBefore);
                Destroy(qhsrBefore);
                Destroy(yzfrAnimator);
                Destroy(yzsrAnimator);
            })
            .Append(jdpzCanvas.DOFade(1, 0.3f))
            .Append(ycwCanvas.DOFade(1, 0.3f))
            .Append(csjCanvas.DOFade(1, 0.3f))
            .Append(srwCanvas.DOFade(1, 0.3f))
            .Append(frwCanvas.DOFade(1, 0.3f))
            .Append(fzCanvas.DOFade(1, 0.3f)) 
            .AppendCallback(() =>
            {
                fzImage.raycastTarget = true;
                jdpzImage.raycastTarget = true;
                jdImage.raycastTarget = true;
                srwImage.raycastTarget = true;
                qhsrImage.raycastTarget = true;
                frwImage.raycastTarget = true;
                qhfrImage.raycastTarget = true;
                ycwImage.raycastTarget = true;
                ycImage.raycastTarget = true;
                csjImage.raycastTarget = true;
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

