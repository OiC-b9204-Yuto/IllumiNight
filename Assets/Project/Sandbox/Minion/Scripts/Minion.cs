using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Minion : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private Vector3 _direction;
    //テストが終わればSerializeFieldは消すこと
    [SerializeField]private BoolReactiveProperty _isBattleRx;
    public IReadOnlyReactiveProperty<bool> IsBattleRx => _isBattleRx;


    void Update()
    {
        if (!IsBattleRx.Value) { Move(); }
    }

    private void Move()
    {
       transform.position += _direction * _movementSpeed * Time.deltaTime; 
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
        //戦闘 -> 時間計測のみ？
        //戦闘開始/終了 -> EventでLightに送る
        //結果を該当エネミーに送る
        _isBattleRx.Value = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            _direction = Vector3.zero;
        }
    }
}