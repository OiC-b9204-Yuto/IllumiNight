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

    Rigidbody _rigidbody;

    [SerializeField] Vector3 _direction;

    Transform _player;

    [SerializeField] Bullet _bulletPrefab;

    [SerializeField] float _trunDegRandom = 90;

    bool _isInMoveArea;

    float _collisionRadius = 1;

    bool _isShotReady = false;
    [SerializeField] float _ShotCoolTime = 3.0f;
    [SerializeField] float _bulletSpeed = 100;

    bool _isCanMove = true;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        float randomDeg = Random.Range(0, 360);
        _direction = Quaternion.Euler(new Vector3(0, randomDeg, 0)) * transform.forward;

        ShotReadyWait().Forget();

        // 死亡時
        IsDead.Subscribe(_ => {
            if (_) 
            { 
                Debug.Log("死亡しました");
                Destroy(this.gameObject, 1.0f);
            } 
        });
    }

    private void Update()
    {
        if (IsDead.Value) { return; }
        //常にプレイヤーの方向を見る
        LookAtPlayer();
        if (_isShotReady)
        {
            ShotAttack();
        }
    }


    void FixedUpdate()
    {
        if (IsDead.Value) { return; }
        if (_isCanMove && _moveSpeed > 0)
        {
            MoveUpdate();
        }
    }

    /// <summary>
    /// プレイヤーの方向を見るための関数
    /// </summary>
    void LookAtPlayer()
    {
        Vector3 lookPos = new Vector3(_player.position.x, transform.position.y, _player.position.z);
        transform.LookAt(lookPos);
    }

    void MoveUpdate()
    {
        if(_moveAreaRadius < (transform.position - _initPosition).magnitude)
        {
            if (_isInMoveArea)
            {
                ChangeDirection();
                _isInMoveArea = false;
            }
        }
        else
        {
            _isInMoveArea = true;
        }
        // 速度の適用
        _rigidbody.velocity = _moveSpeed * Time.fixedDeltaTime * _direction;
    }

    void ChangeDirection()
    {
        Vector3 centerDir = (_initPosition - transform.position);
        centerDir.y = 0;
        centerDir.Normalize();
        float randomDeg = (Random.Range(0, _trunDegRandom) - _trunDegRandom * 0.5f);
        _direction = Quaternion.Euler(new Vector3(0, randomDeg, 0)) * centerDir;
    }

    void ShotAttack()
    {
        _isShotReady = false;
        Bullet bullet = BulletManager.Instance.BulletPool.Get();
        bullet._swing = _bulletPrefab._swing;
        Vector3 bulletPos = transform.position + transform.forward * _collisionRadius;
        Vector3 targetPlayerDir = _player.position - bulletPos;
        targetPlayerDir.y = 0;
        targetPlayerDir.Normalize();
        bullet.Setup(bulletPos, targetPlayerDir, _bulletSpeed);
        ShotReadyWait().Forget();
    }

    /// <summary>
    /// 攻撃のクールタイムを計測してフラグを変える
    /// </summary>
    /// <returns></returns>
    async UniTask ShotReadyWait()
    {
        await UniTask.Delay((int)(_ShotCoolTime * 1000));
        _isShotReady = true;
    }
    public override void BattleStart()
    {
        _isCanMove = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;
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
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, _initPosition);
        }
    }
#endif
}

