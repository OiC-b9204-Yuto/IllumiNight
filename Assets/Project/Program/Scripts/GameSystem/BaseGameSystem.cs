using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_GameSystem : MonoBehaviour
{
    [SerializeField] Vector3[] EnemySpawnPosition;       //敵のスポーン位置
    [SerializeField] int EnemyCount;                     //敵の数
    [SerializeField] int CurrentPhase;                   //現在のフェーズ数
    [SerializeField] float PhaseTime;                    //フェーズの制限時間

    void Start()
    {
        CurrentPhase = 1;
        PhaseTime = 180.0f;
    }

    void Update()
    {
        ChangePhase();
        PhaseSystem();
    }

    void ChangePhase()                                  //フェーズ変更関数
    {
        if (PhaseTime >= 0.0f)                          //フェーズ制限時間以内なら
        {
            PhaseTime -= Time.deltaTime;
        }
        else                                            //フェーズ制限時間以外なら
        {
            CurrentPhase++;
            PhaseTime = 180.0f;
        }
    }

    void PhaseSystem()                                  //フェーズシステム
    {
        switch (CurrentPhase)
        {
            case 1:                                     //フェーズ1
                break;
            case 2:                                     //フェーズ2
                break;
            case 3:                                     //フェーズ3
                break;
        }
    }
}
