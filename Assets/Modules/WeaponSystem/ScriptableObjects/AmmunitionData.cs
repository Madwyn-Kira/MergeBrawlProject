using UnityEngine;

[CreateAssetMenu(fileName = "New Ammunition", menuName = "Ammunition")]
public class AmmunitionData : ScriptableObject
{
    public string ammunitionName;
    public float speed;
    public GameObject projectilePrefab;
}
