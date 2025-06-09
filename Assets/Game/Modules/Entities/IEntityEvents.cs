using System;

public interface IEntityEvents
{
    event Action OnMerge;
    event Action OnStartWar;
    event Action<Entity> OnSpawned;
    event Action<Entity> OnDestroyEntity;
}
