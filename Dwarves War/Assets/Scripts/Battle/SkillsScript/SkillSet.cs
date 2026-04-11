using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitSkills", menuName = "Battle/UnitSkills")]
public class SkillSet : ScriptableObject
{
    [SerializeField] private List<Skill> skills = new List<Skill>();

    public List<Skill> GetSkills()
    {
        return skills;
    }
    // public int GetNumOfSkills()
    // {
    //     return skills.Count;
    // }
}