using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Enemy Config")]
public class EnemyConfig : ConfigSettings
{
    public Entity EnemyPrefab;

    public int MaxHealth = 100;
    public float Speed = 5;
    public float Damage = 4;
}
