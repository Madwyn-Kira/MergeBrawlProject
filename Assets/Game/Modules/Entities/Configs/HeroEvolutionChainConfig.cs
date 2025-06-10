using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Hero Evolution Chain Config")]
public class HeroEvolutionChainConfig : ScriptableObject
{
    [SerializeField] public List<HeroConfig> EvolutionChain = new();
}
