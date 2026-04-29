using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private GameObject textUI;
    [SerializeField] private CanvasGroup canvasGroup;

    public bool ISTexureNull => isTexureNull;

    private bool isTexureNull = true; // 判断当前玩家是否有拿着食材,true 表示当前没有拿, false 表示当前玩家有拿
    private bool isTextEnd = false; // 判断文本是否播放完毕
    private RaycastHit2D hit;
    private Food food;

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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction);
            Debug.Log($"当前玩家拿着食物:{isTexureNull == false}");

            // 判断玩家是否点到食材
            if (Physics2D.Raycast(ray.origin,ray.direction) && hit.collider != null)
            {
                for (int i = 0; i < food.FoodName.Count; i++)
                {
                    if (hit.collider.gameObject.name == food.FoodName[i])
                    {
                        Cursor.SetCursor(food.FoodTextures[i], Vector2.zero, CursorMode.ForceSoftware);
                        textUI.SetActive(true);
                        Text(food.FoodText[i]);
                    }
                }
                
                this.isTexureNull = false;
                Debug.Log($"当前玩家拿着食物:{isTexureNull == false }" );
                Debug.Log("点击的物品是：" + hit.collider.gameObject.name);    
            }
        }

        
        if (Input.GetMouseButtonDown(0) && textUI.activeSelf && isTextEnd)
        {
            textUI.SetActive(false);
            isTextEnd = false;
        }
    }

    // 文字效果
    private void Text(string text)
    {
        canvasGroup.DOFade(1, 1);
        DOTween.To(
            () => "",
            currentText => textMeshProUGUI.text = currentText,
            text,
            1f
         ).SetEase(Ease.Linear).OnComplete(()=> { isTextEnd = true;  Debug.Log("文字播放完毕！"); });
    }
}
