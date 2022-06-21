using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameSystem : MonoBehaviour
{
    [SerializeField] GameObject[] Current_EnemyList;                    //���݂̓G���X�g
    [SerializeField] GameObject[] EnemyObjects;                         //�G��Prefab   
    [SerializeField] int EnemyCount;                                    //�G�̐�
    [SerializeField] int CurrentWave;                                   //���݂̃t�F�[�Y��
    [SerializeField] float WaveTime;                                    //�t�F�[�Y�̐�������

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

    void EnemyCountSystem()                                             //�G�𐔂���V�X�e��
    {
        Current_EnemyList = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyCount = Current_EnemyList.Length;
    }

    void ChangePhase()                                                  //�E�F�[�u�ύX�֐�
    {
        if (WaveTime >= 0.0f)                                           //�E�F�[�u�������Ԉȓ��Ȃ�
        {
            WaveTime -= Time.deltaTime;
        }
        else if(WaveTime <= 0.0f || EnemyCount == 0)                    //�E�F�[�u�������ԈȊO�Ȃ�
        {
            CurrentWave++;
            WaveTime = 180.0f;
        }
    }

    void WaveSystem()                                                  //�E�F�[�u�V�X�e��
    {
        switch (CurrentWave)
        {
            case 1:                                                     //�E�F�[�u1
                break;
            case 2:                                                     //�E�F�[�u2
                break;
            case 3:                                                     //�E�F�[�u3
                break;
            default:                                                    //���̑�
                break;
        }
    }
}
