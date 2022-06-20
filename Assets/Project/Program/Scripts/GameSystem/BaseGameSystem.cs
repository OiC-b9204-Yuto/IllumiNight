using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_GameSystem : MonoBehaviour
{
    [SerializeField] Vector3[] EnemySpawnPosition;       //�G�̃X�|�[���ʒu
    [SerializeField] int EnemyCount;                     //�G�̐�
    [SerializeField] int CurrentPhase;                   //���݂̃t�F�[�Y��
    [SerializeField] float PhaseTime;                    //�t�F�[�Y�̐�������

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

    void ChangePhase()                                  //�t�F�[�Y�ύX�֐�
    {
        if (PhaseTime >= 0.0f)                          //�t�F�[�Y�������Ԉȓ��Ȃ�
        {
            PhaseTime -= Time.deltaTime;
        }
        else                                            //�t�F�[�Y�������ԈȊO�Ȃ�
        {
            CurrentPhase++;
            PhaseTime = 180.0f;
        }
    }

    void PhaseSystem()                                  //�t�F�[�Y�V�X�e��
    {
        switch (CurrentPhase)
        {
            case 1:                                     //�t�F�[�Y1
                break;
            case 2:                                     //�t�F�[�Y2
                break;
            case 3:                                     //�t�F�[�Y3
                break;
        }
    }
}
