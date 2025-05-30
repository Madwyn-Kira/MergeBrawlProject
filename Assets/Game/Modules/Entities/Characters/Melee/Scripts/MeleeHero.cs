public class MeleeHero : HeroController
{
    private void Start()
    {
        base.InitializeParams();
    }

    public override void Fight()
    {

    }

    public override bool TryMerge(HeroController target)
    {
        return base.TryMerge(target);
    }
}
