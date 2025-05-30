using UnityEngine;

public class BoardSpawnCell : MonoBehaviour
{
    [SerializeField]
    public Transform ConcreteHeroTransform;

    [HideInInspector]
    public Entity CurrentHero;

    public bool IsEmpty => CurrentHero == null;

    public void PlaceHero(Entity hero)
    {
        CurrentHero = hero;
        CurrentHero.transform.parent = transform;
        CurrentHero.transform.localScale = new Vector3(1f, 1f, 1f);
        CurrentHero.transform.position = ConcreteHeroTransform.position;
    }

    public void ClearCell()
    {
        CurrentHero = null;
    }
}
