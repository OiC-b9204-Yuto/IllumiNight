using System;

public interface IScore
{
    int Score { get; }
    IObservable<int> ScoreSubject { get; }
}