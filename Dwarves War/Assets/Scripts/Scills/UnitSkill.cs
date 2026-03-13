using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitSkills", menuName = "Battle/UnitSkills")]
public class UnitSkills : ScriptableObject
{
    [SerializeField] private List<Attack> skills = new List<Attack>();

    public List<Attack> GetSkills()
    {
        return skills;
    }
}