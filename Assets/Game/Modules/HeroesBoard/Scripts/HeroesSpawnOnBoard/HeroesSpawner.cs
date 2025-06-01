using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroesSpawner : MonoBehaviour
{
    [SerializeField]
    public List<BoardSpawnCell> spawnCells;
    [SerializeField]
    public List<ConfigSettings> heroConfigs;

    public BoardManager board;

    public void SpawnRandomUnit()
    {
        var freeCell = GetFreeCell();

        if (freeCell != null)
        {
            var heroConfig = heroConfigs[0] as HeroConfig;
            var hero = Instantiate(heroConfig.HeroPrefab);
            var heroEntity = hero.GetComponent<HeroController>();
            heroEntity.InitializeParams();
            heroEntity.Initialize(heroConfigs[0]);

            board.RegisterUnit(heroEntity);
            freeCell.AssignHero(heroEntity);
        }
    }

    private BoardSpawnCell GetFreeCell()
    {
        return spawnCells.Where(item => item.IsEmpty == true).FirstOrDefault();
    }
}
