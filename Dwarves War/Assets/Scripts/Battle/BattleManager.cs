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
    public static  BattleManager Instance{get; private set;}
    private Enemy selectedTarget;
    private enum BattleStage
    {
        Idle,
        PlayerChooseSkill,
        PlayerChooseTarget,
        PlayerAttack,
        EnemyTurn,
        Busy
    }
    private BattleStage battleStage = BattleStage.Busy;
    private void Awake()
    {
        Instance = this;
    }
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
            turnQueue.RemoveAll(u => u == null || !u.IsAlive());
            foreach(Unit u in turnQueue)
            {
                if(u == null || !u.IsAlive())
                    continue;

                if(u is Dwarf dwarf)
                {
                    //Debug.Log("Player start");
                    //dwarf.PrintUnitData();
                    yield return StartCoroutine(PlayerTurn(dwarf));
                    //Debug.Log("Player End");
                }
                else if(u is Enemy enemy)
                {
                    //Debug.Log("Enemy start");
                    //continue;
                    yield return StartCoroutine(EnemyTurn(enemy));
                    //Debug.Log("Enemy End");
                }

                unitManager.CleanDeadUnits();
                u.TickCooldown();
            }

            InitializeBattle();
            yield return null;
        }
    }

    IEnumerator PlayerTurn(Dwarf dwarf)
    {
        battleStage = BattleStage.PlayerChooseSkill;

        //Debug.Log("Problem 1");
        battleUIManager.ShowSkill(dwarf);

        yield return new WaitUntil(() =>
            battleUIManager.GetSelectedSkill() != null
        );

        SkillState skill = battleUIManager.GetSelectedSkill();
        //Debug.Log("Problem 2");
        battleStage = BattleStage.PlayerChooseTarget;

        yield return new WaitUntil(() => selectedTarget != null);

        battleStage = BattleStage.PlayerAttack;
        //Debug.Log("Problem 3");
        dwarf.TryToUseSkill(selectedTarget, skill);

        selectedTarget = null;

        yield return new WaitUntil(() => dwarf.IsIdle());

        battleStage = BattleStage.Idle;
        //Debug.Log("None problem");
    }

    IEnumerator EnemyTurn(Enemy enemy)
    {
        battleStage = BattleStage.EnemyTurn;

        bool didAttack = enemy.TryToAttack(unitManager.dwarfs);

        if(didAttack)
            yield return new WaitUntil(() => enemy.IsIdle());

        battleStage = BattleStage.Idle;
    }

    //потім потрібно додати зберігання скорості атаки щоб лишня скорость атаки зберігалась 
    private void InitializeBattle()
    {
        turnQueue.Clear();
        turnQueue.AddRange(unitManager.GetAllUnits());
        
        if(turnQueue.Count > 1)
            turnQueue.Sort((a,b) => b.GetAttackSpeed().CompareTo(a.GetAttackSpeed()));
    }
    public void TrySelectTarget(Enemy enemy)
    {
        if (battleStage != BattleStage.PlayerChooseTarget)
            return;

        if (enemy == null || !enemy.IsAlive())
            return;

        selectedTarget = enemy;
        Debug.Log("target selected");
    }
}
