using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UniRx;

public class BossEnemy : BaseEnemy
{
    Rigidbody _rigidbody;

    [SerializeField] Vector3 _direction;

    Transform _player;

    [SerializeField] Bullet _bulletPrefab;

    [SerializeField] List<Vector3> teleportPositionList;
    private int nextTeleport = 0;
    bool _isInMoveArea;

    float _collisionRadius = 2;

    bool _isShotReady = false;
    [SerializeField] float _ShotCoolTime = 3.0f;
    [SerializeField] float _bulletSpeed = 100;

    bool _isCanMove = true;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody>();
        CurrentLifePoint.SkipLatestValueOnSubscribe().Where(_ => _ > 0).Subscribe(_ =>
        {
            transform.position = new Vector3(teleportPositionList[nextTeleport].x, transform.position.y, teleportPositionList[nextTeleport].z);
            nextTeleport++;
        });
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

    /// <summary>
    /// プレイヤーの方向を見るための関数
    /// </summary>
    void LookAtPlayer()
    {
        Vector3 lookPos = new Vector3(_player.position.x, transform.position.y, _player.position.z);
        transform.LookAt(lookPos);
    }

    const int shotSplit = 12;

    int shotCount;

    void ShotAttack()
    {
        shotCount++;
        _isShotReady = false;
        float shotDeg = 360 / shotSplit;
        float initDeg = shotCount % 2 * (shotDeg * 0.5f);
        Vector3 shotPos = transform.position;
        shotPos.y = 0.5f;
        for (int i = 0; i < shotSplit; i++)
        {
            float shotRad = (shotDeg * i + initDeg) * Mathf.Deg2Rad;
            Bullet bullet = BulletManager.Instance.BulletPool.Get();
            Vector3 targetPlayerDir = new Vector3(Mathf.Sin(shotRad), 0, Mathf.Cos(shotRad));
            Vector3 bulletPos = shotPos + targetPlayerDir * _collisionRadius;
            bullet.Setup(bulletPos, targetPlayerDir, _bulletSpeed);

        }

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
}

