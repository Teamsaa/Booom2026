using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public bool ISTexureNull => isTexureNull;

    private bool isTexureNull = true; // 判断当前玩家是否有拿着食材,true 表示当前没有拿, false 表示当前玩家有拿
    private RaycastHit2D hit;
    private Texture2D texture2D;

    private void Update()
    {
        Detection();
    }

    private void Start()
    {
    }

    // 鼠标点击检测函数
    private void Detection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction);
            Debug.Log($"当前玩家拿着食物:{isTexureNull == false}");

            if (Physics2D.Raycast(ray.origin,ray.direction) && hit.collider != null)
            {
                Debug.Log(texture2D.ToString());
                Cursor.SetCursor(texture2D, Vector2.zero, CursorMode.ForceSoftware);
                this.isTexureNull = false;
                Debug.Log($"当前玩家拿着食物:{isTexureNull == false }" );
                Debug.Log("点击的物品是：" + hit.collider.gameObject.name);    
            }
        }
    }

    private void Text()
    {

    }
}
