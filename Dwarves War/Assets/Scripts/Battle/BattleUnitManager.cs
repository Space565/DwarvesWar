using System.Collections.Generic;
using UnityEngine;

public class BattleUnitManager : MonoBehaviour
{
    [Header("Units")]
    public List<Dwarf> dwarfs = new List<Dwarf>();
    public List<Enemy> enemies = new List<Enemy>();

    [Header("Positions")]
    public Transform[] dwarfPositions;
    public Transform[] enemyPositions;

    public List<Unit> GetAllUnits()
    {
        CleanDeadUnits();
        List<Unit> all = new List<Unit>();
        all.AddRange(dwarfs);
        all.AddRange(enemies);
        return all;
    }

    public void CleanDeadUnits()
    {
        dwarfs.RemoveAll(d => d == null || !d.IsAlive());
        enemies.RemoveAll(e => e == null || !e.IsAlive());
    }

    public bool HasLivingDwarfs() => dwarfs.Count > 0;
    public bool HasLivingEnemies() => enemies.Count > 0;

    // ------------------ Налаштування позицій ------------------
    public void SetupUnits()
    {
        // Дварфи
        for (int i = 0; i < dwarfs.Count && i < dwarfPositions.Length; i++)
        {
            if (dwarfs[i] != null && dwarfPositions[i] != null)
            {
                dwarfs[i].transform.position = dwarfPositions[i].position;
                dwarfs[i].SetStartPosition(); // юніт запам'ятовує стартову позицію
            }
        }

        // Вороги
        for (int i = 0; i < enemies.Count && i < enemyPositions.Length; i++)
        {
            if (enemies[i] != null && enemyPositions[i] != null)
            {
                enemies[i].transform.position = enemyPositions[i].position;
                enemies[i].SetStartPosition();
            }
        }
    }
}