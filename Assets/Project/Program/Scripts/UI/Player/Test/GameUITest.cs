using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameUITest : MonoBehaviour, IHealth, IScore
{
    ReactiveProperty<int> _health = new ReactiveProperty<int>();
    ReactiveProperty<int> _score = new ReactiveProperty<int>();
    [SerializeField] private int _maxHealth;

    public int Health => _health.Value;
    public IObservable<int> HealthSubject => _health;
    public int MaxHealth => _maxHealth;
    public int Score => _score.Value;
    public IObservable<int> ScoreSubject => _score;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _health.Value++;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _health.Value--;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _score.Value++;
        }
    }
}
