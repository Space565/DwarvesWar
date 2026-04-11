using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Unit
{
    private Vector3 scale;

    protected override void Awake()
    {
        base.Awake();
        scale = transform.localScale;

    }
   
     protected override void FaceTarget(Vector3 target)
    {
        Vector3 scale = transform.localScale;

        if (target.x > transform.position.x)
            scale.x = -Mathf.Abs(scale.x);
        else
            scale.x = Mathf.Abs(scale.x);

        transform.localScale = scale;
    }
    public bool TryToAttack(List<Dwarf> dwarfs)
    {
        if(dwarfs.Count == 0) 
            return false;

        Unit target = dwarfs[UnityEngine.Random.Range(0, dwarfs.Count)];

        SkillState skill = GetStrongestRedySkill();
        if(skill == null)
            return false;

        return TryToUseSkill(target, skill);
    }
    public SkillState GetStrongestRedySkill()
    {
        SkillState best = null;
        foreach(var s in _skillState)
        {
            if(!s.IsReady()) continue;

            if(best == null || s.skill.damage > best.skill.damage)
                best = s;
        }

        if(best == null)
            Debug.Log("Немає готових навиків для атаки");

        return best;
    }


}
