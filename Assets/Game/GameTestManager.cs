using UnityEngine;

public class GameTestManager : MonoBehaviour
{
    public BoardManager HeroesBoard;
    public EnemyBoardController EnemiesBoard;

    public void FightBtn()
    {
        foreach (var item in HeroesBoard.Units)
            item.Fight();

        foreach (var item in EnemiesBoard.Units)
            item.Fight();
    }
}
