using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    public List<BoardSpawnCell> spawnCells;
    [SerializeField]
    public List<ConfigSettings> enemyConfigs;

    public EnemyBoardController board;

    public void SpawnEnemy()
    {
        var freeCell = GetFreeCell();

        if (freeCell != null)
        {
            var enemyConfig = enemyConfigs[0] as EnemyConfig;
            var enemy = Instantiate(enemyConfig.EnemyPrefab);
            var enemyEntity = enemy.GetComponent<Entity>();
            enemyEntity.InitializeParams();
            enemyEntity.Initialize(enemyConfig);

            board.RegisterUnit(enemyEntity);
            freeCell.AssignHero(enemyEntity);
        }
    }

    private BoardSpawnCell GetFreeCell()
    {
        return spawnCells.Where(item => item.IsEmpty == true).FirstOrDefault();
    }
}
