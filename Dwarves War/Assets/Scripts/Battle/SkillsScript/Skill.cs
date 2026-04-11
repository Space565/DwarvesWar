using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Battle/Attack")]
public class Skill : ScriptableObject
{
    public string localizationKey;
    public float damage;
    public float range;
    public int cooldown;
}