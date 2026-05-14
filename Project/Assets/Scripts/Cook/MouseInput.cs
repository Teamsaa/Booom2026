using DG.Tweening;
using DG.Tweening.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;


public class MouseInput : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private GameObject textUI;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Animator animator;
    [SerializeField] private List<GameObject> foodGameObject;

    private bool isTextEnd = false; // 判断文本是否播放完毕
    private RaycastHit2D hit;
    private Food food;
    private TweenerCore<string, string, DG.Tweening.Plugins.Options.StringOptions> textAnimaion;
    private Tween tween;
    private GameObject currentFood = null; // 用来保存当前玩家点击的食物的名字 后续用来判断动画播放和文本内容
    private Transform currentTransform;
    private GameObject clickedObj;

    private void Update()
    {
        Detection();
    }

    private void Start()
    {
        food = FindObjectOfType<Food>();
    }

    // 鼠标点击检测函数
    private void Detection()
    {
        if (Input.GetMouseButtonDown(0) && !textUI.activeSelf)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            // 存储射线碰撞到的所有 UI 结果
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            Debug.Log("点击到的 UI 名称是: " + results[0].gameObject.name);

            if (results.Count > 0 && results != null && results[0].gameObject.GetComponent<Bowl>())
            {
                GameEvent.OnBowlChange?.Invoke(currentFood ?? null, results[0].gameObject);
            }

            // 判断玩家是否点到物品（包括案板、食材）
            if (results.Count > 0 && Cursor.visible)
            {
                for (int i = 0; i < food.FoodName.Count; i++)
                {
                    if (results[0].gameObject.name == food.FoodName[i])
                    {
                        Debug.Log("判断成功！");
                        Cursor.visible = false;
                        clickedObj = results[0].gameObject;
                        currentTransform = clickedObj.transform;
                        if (clickedObj.TryGetComponent<Follow>(out var temp))
                        {
                            temp.isFollow = true;
                        }

                        currentFood = results[0].gameObject;
                        Text(food.FoodText[i]);
                    }
                }
            }
                Debug.Log($"当前玩家拿着食物:{Cursor.visible == false}");
            // 点击到菜板就播放做菜动画
            if (!Cursor.visible && results[0].gameObject.tag == "AnimationTriger")
            {
                Debug.Log(currentFood);

                for (int i = 0; i < foodGameObject.Count; i++)
                {
                    if (currentFood.name == foodGameObject[i].gameObject.name)
                    {
                        foodGameObject[i].SetActive(true);
                        animator = foodGameObject[i].GetComponent<Animator>();
                        StartCoroutine(FoodAnimationAndWait(i, foodGameObject[i].gameObject.name, clickedObj));
                    }
                }
            }
        }

        // 这里是快速播放完Dotween文字动画效果的逻辑 和 重置Dotween动画效果
        if (Input.GetMouseButtonDown(0) && textUI.activeSelf && isTextEnd)
        {
            canvasGroup.alpha = 0;
            textMeshProUGUI.text = "";
            textUI.SetActive(false);
            isTextEnd = false;

        }
        else if (Input.GetMouseButtonDown(0) && textUI.activeSelf && !isTextEnd)
        {
            Debug.Log("发现当前动画未完成，直接结束！");
            textAnimaion.Complete();
        }

    }

    #region Dotween做的文字动画效果
    // 文字效果
    private void Text(string text)
    {
        if (text == "") return;

        textUI.SetActive(true);
        tween = canvasGroup.DOFade(1, 1);
        StartCoroutine(TextAnimation(text));
    }

    IEnumerator TextAnimation(string text)
    {
        yield return new WaitForSeconds(0.5f);
        this.textAnimaion = DOTween.To(
           () => "",
           currentText => textMeshProUGUI.text = currentText,
           text,
           2f
        ).SetEase(Ease.Linear).OnComplete(() => { isTextEnd = true; Debug.Log("文字播放完毕！"); });
    }
    #endregion

    /// <summary>
    /// 播放动画的组件
    /// </summary>
    /// <param name="index">传入参数，表示当前应该播放那个菜的动画</param>
    /// <param name="name">菜的名字</param>
    /// <param name="clickedObj">相对应的菜的obj</param>
    /// <returns></returns>

    IEnumerator FoodAnimationAndWait(int index, string name, GameObject clickedObj)
    {
        animator.Play(name);
        bool isDestory = true;
        if (clickedObj.TryGetComponent<Follow>(out var temp))
        {
            temp.SetAnimationNext(name, true, out isDestory);
        }

        yield return null;

        // 判断动画是否播完
        while (true)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log(stateInfo.IsName(name));
            if (stateInfo.IsName(name))
            {
                if (stateInfo.normalizedTime >= 1.0f)
                    break;
            }

            yield return null;
        }

        foodGameObject[index].SetActive(false);
        Debug.Log($"是否销毁：{isDestory == true}");
        if (isDestory)
            Destroy(clickedObj);
        else
            temp.isFollow = false;
            clickedObj.transform.position = currentTransform.position;
        Cursor.visible = true;
        temp.SetAnimationNext(name, false, out isDestory);
    }
}
