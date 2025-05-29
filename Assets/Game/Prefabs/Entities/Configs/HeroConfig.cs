using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Hero Config")]
public class HeroConfig : ScriptableObject
{
    public string heroName;
    public HeroType heroType;
    public int baseHealth;
    public int baseDamage;
    public GameObject prefab;
}
