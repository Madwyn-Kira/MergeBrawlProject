public class MeleeHero : Entity
{
    private void Start()
    {
        base.InitializeParams();
    }

    public override void Fight()
    {

    }

    public override bool TryMerge(Entity target)
    {
        return base.TryMerge(target);
    }
}
