using System;

public interface IFightCycle
{
    public event Action OnStartPreparation;
    public event Action OnStartFight;
    public event Action<bool> OnEndFight;
}
