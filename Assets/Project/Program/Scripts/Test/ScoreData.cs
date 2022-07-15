using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScoreData : ScriptableObject
{
    const int DEpoint = 600;
    const int RDpoint = -150;

    [SerializeField]
    private int _defeatedEnemy = 0;
    [SerializeField]
    private int _receivedDamage = 0;

    public int DefeatedEnemy { get; set; }
    public int ReceivedDamage { get; set; }

    private void OnEnable()
    {
        DefeatedEnemy = _defeatedEnemy;
        ReceivedDamage = _receivedDamage;
    }

    public int GetScore()
    {
        int score;

        score = DefeatedEnemy * DEpoint + ReceivedDamage * RDpoint;

        return score;
    }
}
