using System;

public interface IEntityEvents
{
    event Action OnMerge;
    event Action<Entity> OnSpawned;
    event Action<Entity> OnKilled;
    event Action<Entity> OnDestroy;
}
