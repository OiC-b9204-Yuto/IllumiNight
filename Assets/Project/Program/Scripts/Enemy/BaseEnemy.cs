using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 敵のベースクラス 作成者：竹中
/// </summary>
public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected int _initLifePoint;

    [SerializeField] protected IntReactiveProperty _currentLifePoint;
    /// <summary>
    /// 現在のライフポイント
    /// </summary>
    public IReadOnlyReactiveProperty<int> CurrentLifePoint => _currentLifePoint;

    [SerializeField] protected float _requiredBattleTime;
    /// <summary>
    /// ダメージを受けるまでに必要な戦闘時間
    /// </summary>
    public float RequiredBattleTime => _requiredBattleTime;

  
    [SerializeField] protected BoolReactiveProperty _isDead;
    /// <summary>
    /// 死亡用フラグ
    /// </summary>  
    public IReadOnlyReactiveProperty<bool> IsDead => _isDead;

    protected Vector3 _initPosition;
    /// <summary>
    /// 初期座標
    /// </summary>
    public Vector3 InitPosition => _initPosition;

    /// <summary>
    /// 初期化用関数<br/>
    /// フィールドの初期化などを行っている
    /// </summary>
    protected virtual void Initialize()
    {
        _currentLifePoint = new IntReactiveProperty(_initLifePoint);
        _isDead = new BoolReactiveProperty(false);
        CurrentLifePoint.Where(_ => _ <= 0).Subscribe(_ => _isDead.Value = true);
        _initPosition = transform.position;
    }

    /// <summary>
    /// ダメージを与える関数<br/>
    /// ※戦闘時間の計測はミニオン側で行う
    /// </summary>
    public void TakeDamage()
    {
        _currentLifePoint.Value--;
    }

    public abstract void BattleStart();

    protected virtual void Awake()
    {
        Initialize();
    }
}
