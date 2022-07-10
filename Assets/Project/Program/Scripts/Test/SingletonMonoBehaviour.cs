using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// シングルトンパターンのMonoBehaviourクラス
/// </summary>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);
                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogError(t + "をアタッチしているGameObjectが見つかりませんでした");
                }
            }
            return instance;
        }
    }
    protected virtual void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            Debug.LogWarning(typeof(T) + "は既に他のGameObjectにアタッチされてます" + Instance.gameObject.name);
            return;
        }
    }
    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}