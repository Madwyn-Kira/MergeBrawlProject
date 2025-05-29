using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Hero Evolution Chain Config")]
public class HeroEvolutionChainConfig : ScriptableObject
{
    public List<HeroConfig> EvolutionChain = new();
}
