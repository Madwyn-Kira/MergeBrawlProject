using System;
using UnityEngine;

public class FightCycleController : MonoBehaviour, IFightCycle
{
    [SerializeField]
    private BoardManager HeroesBoard;
    [SerializeField]
    private EnemyBoardController EnemiesBoard;

    public event Action OnStartPreparation;
    public event Action OnStartFight;
    public event Action<bool> OnEndFight;

    public void FightBtn()
    {
        foreach (var item in HeroesBoard.Units)
            item.Fight();

        foreach (var item in EnemiesBoard.Units)
            item.Fight();

        OnStartFight?.Invoke();
    }

    public void EndFight(bool isHeroesWin)
    {
        OnEndFight?.Invoke(isHeroesWin);
    }

    public void DoPreparation()
    {
        OnStartPreparation?.Invoke();
    }
}
