using System.Collections.Generic;
using UnityEngine;

public class EnemyBoardController : MonoBehaviour
{
    public EnemySpawner Spawner;

    private List<Entity> units = new();

    public void RegisterUnit(Entity unit) => units.Add(unit);
    public void UnregisterUnit(Entity unit) => units.Remove(unit);
}
