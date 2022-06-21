using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameSystem : MonoBehaviour
{
    [SerializeField] GameObject[] Current_EnemyList;                    //現在の敵リスト
    [SerializeField] GameObject[] EnemyObjects;                         //敵のPrefab   
    [SerializeField] int EnemyCount;                                    //敵の数
    [SerializeField] int CurrentWave;                                   //現在のフェーズ数
    [SerializeField] float WaveTime;                                    //フェーズの制限時間

    void Start()
    {
        CurrentWave = 1;
        WaveTime = 180.0f;
    }

    void Update()
    {
        EnemyCountSystem();
        ChangePhase();
        WaveSystem();
    }

    void EnemyCountSystem()                                             //敵を数えるシステム
    {
        Current_EnemyList = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyCount = Current_EnemyList.Length;
    }

    void ChangePhase()                                                  //ウェーブ変更関数
    {
        if (WaveTime >= 0.0f)                                           //ウェーブ制限時間以内なら
        {
            WaveTime -= Time.deltaTime;
        }
        else if(WaveTime <= 0.0f || EnemyCount == 0)                    //ウェーブ制限時間以外なら
        {
            CurrentWave++;
            WaveTime = 180.0f;
        }
    }

    void WaveSystem()                                                  //ウェーブシステム
    {
        switch (CurrentWave)
        {
            case 1:                                                     //ウェーブ1
                break;
            case 2:                                                     //ウェーブ2
                break;
            case 3:                                                     //ウェーブ3
                break;
            default:                                                    //その他
                break;
        }
    }
}
