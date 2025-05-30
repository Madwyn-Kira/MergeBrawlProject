public class MeleeHero : Entity
{
    private void Start()
    {
        base.InitializeParams();
    }

    public override void Fight()
    {

    }

    public override bool Merge(Entity target)
    {
        return base.Merge(target);
    }
}
