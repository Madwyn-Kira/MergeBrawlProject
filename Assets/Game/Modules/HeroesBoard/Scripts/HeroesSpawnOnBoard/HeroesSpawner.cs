using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroesSpawner : MonoBehaviour
{
    [SerializeField]
    public List<BoardSpawnCell> spawnCells;
    [SerializeField]
    public List<HeroConfig> heroConfigs;

    [HideInInspector]
    public BoardManager board;

    public void SpawnRandomUnit()
    {
        var freeCell = GetFreeCell();

        if (freeCell != null)
        {
            var hero = Instantiate(heroConfigs[0].HeroPrefab);
            var heroEntity = hero.GetComponent<Entity>();
            heroEntity.Initialize(heroConfigs[0]);
            heroEntity.ChangeCell(freeCell);
            //freeCell.PlaceHero(heroEntity);
        }
    }

    private BoardSpawnCell GetFreeCell()
    {
        return spawnCells.Where(item => item.IsEmpty == true).FirstOrDefault();
    }
}
