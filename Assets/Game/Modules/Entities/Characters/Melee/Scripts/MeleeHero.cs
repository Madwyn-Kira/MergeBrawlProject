public class MeleeHero : HeroController
{
    public override void Fight()
    {
        ChangeState(new MeleeHeroAttackState());
    }

    public override bool TryMerge(HeroController target)
    {
        return base.TryMerge(target);
    }

    private void OnDestroy()
    {
        DestroyEntity();
    }
}
