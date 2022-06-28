using System;

public interface IHealth
{
    int Health { get; }
    IObservable<int> HealthSubject { get; }
    int MaxHealth { get; }
}