using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public bool ISTexureNull => isTexureNull;

    [SerializeField] private FoodImage foodImage;
    private bool isTexureNull = true; // 判断当前玩家是否有拿着食材,true 表示当前没有拿, false 表示当前玩家有拿
    private RaycastHit2D hit;

    private void Update()
    {
        Detection();
    }

    // 鼠标点击检测函数
    private void Detection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log($"当前玩家拿着食物:{isTexureNull == false}");

            if (Physics2D.Raycast(ray.origin,ray.direction))
            {
                hit = Physics2D.Raycast(ray.origin, ray.direction);
                Cursor.SetCursor(foodImage.foodList[0], Vector2.zero, CursorMode.Auto);
                this.isTexureNull = false;
                Debug.Log($"当前玩家拿着食物:{isTexureNull == false }" );
                Debug.Log("点击的物品是：" + hit.collider.gameObject.name);    
            }
        }
    }
}
