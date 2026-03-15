using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using NUnit.Framework.Constraints;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private BattleUnitManager unitManager;
    [SerializeField] private BattleUIManager battleUIManager;

    private List<Unit> turnQueue = new List<Unit>();

    private enum BattleStage
    {
        None,
        ChooseSkill,
        ChooseTarget,
        EnemyAttack
    }
    private BattleStage battleStage = BattleStage.None;
    private void Start()
    {
        unitManager.SetupUnits();
        InitializeBattle();
        StartCoroutine(CombatSystem());
    }

    private IEnumerator CombatSystem()
    {
        while(unitManager.HasLivingDwarfs() && unitManager.HasLivingEnemies())
        {
            switch(battleStage)
            {
                case BattleStage.None:
                    foreach(Unit u in turnQueue)
                    {
                        if(u == null || !u.IsAlive())
                            continue;

                        if(u is Dwarf dwarf)
                        continue;
                            //yield return StartCoroutine(PlayerTurn(dwarf));
                        else if(u is Enemy enemy)
                            yield return StartCoroutine(UnitTurnRoutine(enemy));

                        unitManager.CleanDeadUnits();
                        u.TickCooldown();
                    }
                    break;
                case BattleStage.ChooseSkill:
                    break;
                case BattleStage.ChooseTarget:
                    break;
                case BattleStage.EnemyAttack:
                    break;
            }
           

            InitializeBattle();
            yield return null;
        }
    }

//   IEnumerator PlayerTurn(Dwarf dwarf)
//    {
//        battleUIManager.ShowSkill(dwarf);
//
//        yield return new WaitUntil(() => battleUIManager.GetSelectedSkill() != null);
//
//        SkillState skill = battleUIManager.GetSelectedSkill();
//
//        // ТЕПЕР включаємо режим вибору ворога
//        battleUIManager.EnableTargetSelection(unitManager.enemies);
//
//        yield return new WaitUntil(() => battleUIManager.GetSelectedTarget() != null);
//
//        Unit target = battleUIManager.GetSelectedTarget();
//
//        dwarf.TryToUseSkill(target, skill);
//
//        battleUIManager.ResetTarget();
//
//        yield return new WaitUntil(() => dwarf.IsIdle());
//    }
    private IEnumerator UnitTurnRoutine(Enemy enemy)
    {
        bool didAttack = enemy.TryToAttack(unitManager.dwarfs);
        if(!didAttack) yield break;

        yield return new WaitUntil(() => enemy.IsIdle());
    }

    //потім потрібно додати зберігання скорості атаки щоб лишня скорость атаки зберігалась 
    private void InitializeBattle()
    {
        turnQueue.Clear();
        turnQueue.AddRange(unitManager.GetAllUnits());

        if(turnQueue.Count > 1)
            turnQueue.Sort((a,b) => b.GetAttackSpeed().CompareTo(a.GetAttackSpeed()));
    }
}
