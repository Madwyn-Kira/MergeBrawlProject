using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroesSpawner : MonoBehaviour
{
    [SerializeField]
    public List<BoardSpawnCell> spawnCells;
    [SerializeField]
    public List<HeroConfig> heroConfigs;

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

            freeCell.AssignHero(heroEntity);
            board.RegisterUnit(heroEntity);
        }
    }

    public void SpawnInCellWithNewConfig(BoardSpawnCell cell, HeroConfig config)
    {
        //if (!cell.IsEmpty) return;

        var heroConfig = config;
        var hero = Instantiate(heroConfig.HeroPrefab);
        var heroEntity = hero.GetComponent<HeroController>();
        heroEntity.InitializeParams();
        heroEntity.Initialize(heroConfig);

        cell.AssignHero(heroEntity);
        board.RegisterUnit(heroEntity);
    }

    private BoardSpawnCell GetFreeCell()
    {
        return spawnCells.Where(item => item.IsEmpty == true).FirstOrDefault();
    }
}
