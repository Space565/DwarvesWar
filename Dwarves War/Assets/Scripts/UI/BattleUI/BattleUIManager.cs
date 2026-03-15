using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject attackButtonPrefab;
    [SerializeField] private Transform attackPanel;
    [SerializeField] private LocalizationDatabase database;
    private Unit currentUnit;
    private SkillState selectedSkill;
    private int lang;

    public void ShowSkill(Unit unit)
    {
        if(unit == null)
        {
            Debug.LogError("Unit == NULL");
            return;
        }
    
        if(unit.skillSet == null)
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
        selectedSkill = null;
    
        foreach (Transform child in attackPanel)
            Destroy(child.gameObject);

        float landslide = 0f;
        foreach (var skill in unit.GetRuntimeSkills())
        {
            GameObject btn = Instantiate(attackButtonPrefab, attackPanel, false);

            // ---------- ВКЛЮЧАЄМО КНОПКУ ----------
            btn.SetActive(true);
            Button button = btn.GetComponent<Button>();
            if(button != null)
                button.enabled = true;
            Image image = btn.GetComponent<Image>();
            if(image != null)
                image.enabled = true;
            TextMeshProUGUI text = btn.GetComponentInChildren<TextMeshProUGUI>(true);
            if(text != null)
            {
                text.enabled = true;
                text.gameObject.SetActive(true);
                text.text = database.GetText(skill.skill.localizationKey, lang);
            }
            else
                Debug.LogError("TMP не знайдений в prefab");

            // ---------- ЗСУВ ANCHOR ВЛІВО ----------
            RectTransform rt = btn.GetComponent<RectTransform>();

            Vector2 min = rt.anchorMin;
            Vector2 max = rt.anchorMax;

            min.x -= landslide;
            max.x -= landslide;

            landslide += 0.16f;

            rt.anchorMin = min;
            rt.anchorMax = max;

            // ---------- CLICK ----------
            button.onClick.RemoveAllListeners();
            button.interactable = skill.IsReady();
            button.onClick.AddListener(() =>
            {
                SelectAttack(skill);
            });
        }
    }

    private void SelectAttack(SkillState state)
    {
        selectedSkill = state;
        Debug.Log("Обрана атака: " + state.skill.localizationKey);
    }

    public SkillState GetSelectedSkill()
    {
        return selectedSkill;
    }
}
