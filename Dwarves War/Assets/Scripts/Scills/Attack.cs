using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Battle/Attack")]
public class Attack : ScriptableObject
{
    public string localizationKey;
    public float damage;
    public float range;
    public float cooldown;
}