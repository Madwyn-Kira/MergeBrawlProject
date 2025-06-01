using Datasaver;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour, IBoard
{
    public HeroesSpawner Spawner;

    private List<Entity> units = new();
    private DataSaver saver = new DataSaver();

    public List<Entity> Units { get { return units; } }

    public void RegisterUnit(Entity unit) => units.Add(unit);
    public void UnregisterUnit(Entity unit) => units.Remove(unit);

    private void Awake()
    {

    }

    private void Start()
    {

    }

    private void LoadOldBoard()
    {

    }

    private void SaveBoard()
    {

    }
}
