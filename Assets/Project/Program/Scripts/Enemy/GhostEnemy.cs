using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEditor;

public class GhostEnemy : BaseEnemy
{
    [SerializeField] private float _moveSpeed;


    [SerializeField] float _moveAreaRadius;

    int _movePointIndex = 0;
    bool moveNext = false;
    void Start()
    {
        // 死亡時
        IsDead.Subscribe(_ => {
            if (_) { Debug.Log("死亡しました"); } 
        });
    }

    void Update()
    {
        MoveUpdate();
    }

    void MoveUpdate()
    {
        // 円形の移動範囲内をランダムに移動する
    }

    void AttackUpdate()
    {
        
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        //移動範囲円の描画
        Handles.color = Color.yellow;
        if (!EditorApplication.isPlaying)
        {
            Handles.DrawWireDisc(transform.position, Vector3.up, _moveAreaRadius);
        }
        else
        {
            Handles.DrawWireDisc(_initPosition, Vector3.up, _moveAreaRadius);
        }
    }
#endif
}

