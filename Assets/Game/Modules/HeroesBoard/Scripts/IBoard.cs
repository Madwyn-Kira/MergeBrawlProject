public interface IBoard
{
    public void RegisterUnit(Entity unit);
    public void UnregisterUnit(Entity unit, bool isDead = false);
    public void UnitMoveCell(Entity unit, BoardSpawnCell oldCell, BoardSpawnCell newCell);
}
