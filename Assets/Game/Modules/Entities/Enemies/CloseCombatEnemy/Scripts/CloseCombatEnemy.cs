public class CloseCombatEnemy : EnemyController
{
    public override void Fight()
    {
        ChangeState(new CloseCombatFindAttackTargetState());
    }

    private void OnDestroy()
    {
        DestroyEntity();
    }
}
