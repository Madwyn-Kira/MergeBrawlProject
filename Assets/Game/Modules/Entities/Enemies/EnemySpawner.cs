using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyController> enemiesList = new();

    [SerializeField]
    public List<BoardSpawnCell> spawnCells;

    [HideInInspector]
    public BoardManager board;

    public void SpawnEnemy()
    {
        var freeCell = GetFreeCell();

        if (freeCell != null)
        {
            var hero = Instantiate(heroConfigs[0].HeroPrefab);
            var heroEntity = hero.GetComponent<HeroController>();
            heroEntity.InitializeParams();
            heroEntity.Initialize(heroConfigs[0]);

            freeCell.AssignHero(heroEntity);
        }
    }

    private BoardSpawnCell GetFreeCell()
    {
        return spawnCells.Where(item => item.IsEmpty == true).FirstOrDefault();
    }
}
