public class MeleeHero : HeroController
{
    public override void Fight()
    {

    }

    public override bool TryMerge(HeroController target)
    {
        return base.TryMerge(target);
    }
}
