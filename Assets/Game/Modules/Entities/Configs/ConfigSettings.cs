using System;
using UnityEngine;

[Serializable]
public abstract class ConfigSettings : ScriptableObject
{
    [SerializeField] public Vector3 ScaleForSpawn;
}
