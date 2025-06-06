using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Hero Config")]
public class HeroConfig : ConfigSettings
{
    public string heroName;
    public HeroType heroType;
    public int baseHealth;
    public int baseDamage;
    public HeroEvolutionChainConfig evolutionConfig;
    public GameObject HeroPrefab;
}
