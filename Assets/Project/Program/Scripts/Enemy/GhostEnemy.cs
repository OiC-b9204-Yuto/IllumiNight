using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;

public class GhostEnemy : BaseEnemy
{
    [SerializeField] private float _moveSpeed;

    // ˆÚ“®‚ÍÀ•Ww’è
    [SerializeField] List<MoveData> _moveData;
    int _movePointIndex = 0;
    bool moveNext = false;
    void Start()
    {
        // €–S
        IsDead.Subscribe(_ => {
            if (_) { Debug.Log("€–S‚µ‚Ü‚µ‚½"); } 
        });

        if (_moveData.Count <= 0) { return; }
        MoveTimer(_moveData[_movePointIndex].time, this.GetCancellationTokenOnDestroy()).Forget();
    }

    void Update()
    {
        MoveUpdate();
    }

    async UniTask MoveTimer(float time, CancellationToken cancellationToken)
    {
        await UniTask.Delay((int)(time * 1000));
        moveNext = true;
    }

    void MoveUpdate()
    {
        if (_moveData.Count <= 0) { return; }

        transform.position += _moveSpeed * Time.deltaTime * _moveData[_movePointIndex].dir.normalized;

        if (moveNext)
        {
            _movePointIndex++;
            if (_moveData.Count <= _movePointIndex)
            {
                _movePointIndex -= _moveData.Count;
            }
            MoveTimer(_moveData[_movePointIndex].time, this.GetCancellationTokenOnDestroy()).Forget();
            moveNext = false;
        }
    }

    void MovePointCheck()
    {
        
    }

    void AttackUpdate()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        for (int i = 0; i < _moveData.Count; i++)
        {
            Gizmos.color = Color.blue;
            Vector3 nextpos = pos + _moveData[i].time * _moveSpeed * _moveData[i].dir.normalized;
            Gizmos.DrawLine(pos, nextpos);
            pos = nextpos;
        }
    }
}

[System.Serializable]
public class MoveData
{
    public Vector3 dir;
    public float time;
}

