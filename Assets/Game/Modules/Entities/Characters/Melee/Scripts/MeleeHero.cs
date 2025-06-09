public class MeleeHero : HeroController
{
    public override void Fight()
    {
        ChangeState(new MeleeHeroAttackState());
    }

    public override bool TryMerge(HeroController target, HeroesSpawner spawner)
    {
        return base.TryMerge(target, spawner);
    }

    private void OnDestroy()
    {
        DestroyEntity();
    }
}
