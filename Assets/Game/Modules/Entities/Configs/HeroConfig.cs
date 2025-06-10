using Newtonsoft.Json;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Hero Config")]
public class HeroConfig : ConfigSettings
{
    public string heroName;
    public HeroType heroType;
    public int baseHealth;
    public int baseDamage;
    public HeroEvolutionChainConfig evolutionConfig;

    [JsonIgnore] public GameObject HeroPrefab;
}
