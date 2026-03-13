using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiBattleManager : MonoBehaviour
{
    [SerializeField] private GameObject attackButtonPrefab;
    [SerializeField] private Transform attackPanel;
    [SerializeField] private LocalizationDatabase database;
    // нище [SerializeField] убрати
    [SerializeField] private Unit currentUnit;
    private Attack selectedAttack;
    private int lang; //= PlayerPrefs.GetInt("language");


    private void Start()
    {
       lang = PlayerPrefs.GetInt("language");

        if(currentUnit != null)
            ShowAttacks(currentUnit);
        else
        Debug.LogError("Current Unit НЕ назначений");
    }
    public void ShowAttacks(Unit unit)
    {
        if(unit == null)
        {
            Debug.LogError("Unit == NULL");
            return;
        }
    
        if(unit.unitSkills == null)
        {
            Debug.LogError("UnitSkills == NULL");
            return;
        }
    
        if(attackPanel == null)
        {
            Debug.LogError("AttackPanel == NULL");
            return;
        }
    
        currentUnit = unit;
        selectedAttack = null;
    
        foreach (Transform child in attackPanel)
            Destroy(child.gameObject);
    
        foreach (var attack in unit.unitSkills.GetSkills())
        {
            GameObject btn = Instantiate(attackButtonPrefab, attackPanel, false);
    
            TextMeshProUGUI text = btn.GetComponentInChildren<TextMeshProUGUI>();
    
            if(text != null)
                text.text = database.GetText(attack.localizationKey, lang);
            else
                Debug.LogError("TMP не знайдений в prefab");
    
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                SelectAttack(attack);
            });
        }
    }

    private void SelectAttack(Attack attack)
    {
        selectedAttack = attack;
        Debug.Log("Обрана атака: " + attack.localizationKey);
    }

    public Attack GetSelectedAttack()
    {
        return selectedAttack;
    }
}
