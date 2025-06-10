using System.Collections.Generic;
using UnityEngine;

public class EnemyBoardController : MonoBehaviour, IBoard
{
    public EnemySpawner Spawner;

    private List<Entity> units = new();
    public List<Entity> Units { get { return units; } }

    public void RegisterUnit(Entity unit) => units.Add(unit);

    public void UnitMoveCell(Entity unit, BoardSpawnCell oldCell, BoardSpawnCell newCell)
    {

    }

    public void UnregisterUnit(Entity unit, bool isDead = false) => units.Remove(unit);

    private void Start()
    {
        for (int i = 0; i < 3; i++)
            Spawner.SpawnEnemy();
    }
}
