using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> :MonoBehaviour where T : Singleton<T>
{
    protected static T instance;
    
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    instance = go.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    // 确保运行周期中只有一个单例
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            Init();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // 初始化类
    protected virtual void Init() { }
}
