using Datasaver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class BoardManager : MonoBehaviour, IBoard
{
    public HeroesSpawner Spawner;

    private List<Entity> units = new();
    private DataSaver saver;

    public List<Entity> Units { get { return units; } }

    public void UnitMoveCell(Entity unit, BoardSpawnCell oldCell, BoardSpawnCell newCell)
    {
        HeroConfig heroConfig = unit.ConfigSettings as HeroConfig;

        var hero = HeroesForSaveBoard.Where(item => item.HeroType == heroConfig.heroType && item.CellID == Spawner.spawnCells.IndexOf(oldCell)).FirstOrDefault();

        if (hero == null) return;

        hero.CellID = Spawner.spawnCells.IndexOf(newCell);
        SaveBoard();
    }

    public void RegisterUnit(Entity unit)
    {
        units.Add(unit);
        HeroConfig heroConfig = unit.ConfigSettings as HeroConfig;

        var hero = HeroesForSaveBoard.Where(item => item.HeroType == heroConfig.heroType && item.CellID == Spawner.spawnCells.IndexOf(unit.CurrentCell)).FirstOrDefault();
        if (hero == null)
            HeroesForSaveBoard.Add(new HeroOnBoardStruct(heroConfig.heroType, Spawner.spawnCells.IndexOf(unit.CurrentCell)));

        SaveBoard();
    }

    public void UnregisterUnit(Entity unit, bool isDead = false)
    {
        units.Remove(unit);

        if (isDead) return;

        HeroConfig heroConfig = unit.ConfigSettings as HeroConfig;
        var hero = HeroesForSaveBoard.Where(item => item.HeroType == heroConfig.heroType).FirstOrDefault();

        if (hero != null)
        {
            HeroesForSaveBoard.Remove(hero);
            SaveBoard();
        }

    }

    private List<HeroOnBoardStruct> HeroesForSaveBoard = new();

    private async void Awake()
    {
        saver = new DataSaver();

        await LoadOldBoard();
    }

    private async Task LoadOldBoard()
    {
        HeroesForSaveBoard.AddRange(await saver.Deserialize<HeroOnBoardStruct>("/board.json"));

        foreach (var item in HeroesForSaveBoard)
        {
            Spawner.SpawnInCellWithNewConfig(Spawner.spawnCells[item.CellID], Spawner.heroConfigs.Where(item1 => item1.heroType == item.HeroType).FirstOrDefault()/*item.Config*/);
        }
    }

    private async void SaveBoard()
    {
        await saver.Serialize(HeroesForSaveBoard, "/board.json");
    }
}
