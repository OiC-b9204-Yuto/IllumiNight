using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Minion : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    //件の討伐時間を攻撃力依存にする場合のもの
    [SerializeField] private int _attackPoint;
    private Vector3 _direction;
    //テストが終わればSerializeFieldは消すこと
    [SerializeField]private BoolReactiveProperty _isBattleRx;
    public IReadOnlyReactiveProperty<bool> IsBattleRx => _isBattleRx;

    private float _timer = 0;

    void Update()
    {
        if (!IsBattleRx.Value) { Move(); }
        else { CountTimer(); }
    }

    private void Move()
    {
       transform.position += _direction * _movementSpeed * Time.deltaTime; 
    }

    private void CountTimer()
    {
        _timer -= Time.deltaTime * _attackPoint;
        if(_timer <= 0) { BattleEnd(); }
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsBattleRx.Value) { return; }

        if(other.gameObject.tag == "Enemy")
        {
            TriggerEnemy(other);
        }
        else if (other.gameObject.tag == "Light")
        {
            TriggerLight(other);
        }
    }

    private void TriggerLight(Collider collider)
    {
        Vector3 dir = transform.position - collider.transform.position;
        dir.y = 0;
        dir.Normalize();
        if (dir == Vector3.zero)
        {
            dir.z = 1;
        }
        _direction = dir;
    }
    private void TriggerEnemy(Collider collider)
    {
        //敵情報取得 -> 終了時結果を渡すため(BaseEnemy)
        //戦闘 -> 時間計測のみ？ _timerに戦闘時間を
        //戦闘開始/終了 -> EventでLightに送る
        _isBattleRx.Value = true;
    }

    private void BattleEnd()
    {
        _isBattleRx.Value = false;
        //敵に戦闘終了を送る
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            _direction = Vector3.zero;
        }
    }
}