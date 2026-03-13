using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    public Transform[] dwarfPositions;
    public Transform[] enemyPositions;
    public List<Dwarf> dwarfs = new List<Dwarf>();
    public List<Enemy> enemies = new List<Enemy>();
    private List<Unit> turnQueue = new List<Unit>();


    private void Start()
    {
        InitializeBattle();
        StartCoroutine(CombatSystem());
    }

    void SetupUnits()
    {
       for (int i = 0; i < dwarfs.Count && i < dwarfPositions.Length; i++)
    {
        if(dwarfPositions[i] != null)
        {
            dwarfs[i].transform.position = dwarfPositions[i].position;
            dwarfs[i].SetStartPosition();
        }
    }

    for (int i = 0; i < enemies.Count && i < enemyPositions.Length; i++)
    {
        if(enemyPositions[i] != null)
        {
            enemies[i].transform.position = enemyPositions[i].position;
            enemies[i].SetStartPosition();
        }
    }
    }
    
    private IEnumerator CombatSystem()
    {
        SetupUnits();

        while(dwarfs.Count > 0 && enemies.Count > 0)
        {
            foreach(Unit u in turnQueue)
            {
                if(!u.IsAlive())
                    continue;

                if(u is Dwarf dwarf)
                    continue;
                else if(u is Enemy enemy)
                    yield return StartCoroutine(UnitTurnRoutine(enemy));

                RemoveDeadUnits();
            }

            InitializeBattle();
            yield return null;
        }
    }

    private IEnumerator UnitTurnRoutine(Unit unit)
    {
        Unit target = null;
        if(unit is Dwarf d && enemies.Count > 0)
            target = enemies[Random.Range(0, enemies.Count)];
        else if(unit is Enemy e && dwarfs.Count > 0)
            target = dwarfs[Random.Range(0, dwarfs.Count)];

        if(target == null)
            yield break;

        unit.StartAttack(target);

        yield return new WaitUntil(() => unit.IsIdle());
    }

    //потім потрібно додати зберігання скорості атаки щоб лишня скорость атаки зберігалась 
    public void InitializeBattle()
    {   
        turnQueue.Clear();

        turnQueue.AddRange(dwarfs);
        turnQueue.AddRange(enemies);

        if(turnQueue.Count > 1)
            turnQueue.Sort((a,b) => b.GetAttackSpeed().CompareTo(a.GetAttackSpeed()));
    }
    
    private void RemoveDeadUnits()
    {
        for(int i = dwarfs.Count - 1; i >= 0; i--)
        {
            if(!dwarfs[i].IsAlive())
                dwarfs.RemoveAt(i);
        }

        for(int i = enemies.Count - 1; i >= 0; i--)
        {
            if(!enemies[i].IsAlive())
                enemies.RemoveAt(i);
        }
    }
}
