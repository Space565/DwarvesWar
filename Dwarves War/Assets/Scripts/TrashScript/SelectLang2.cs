using UnityEngine;
using UnityEngine.UI;

public class LanguageDropdown : MonoBehaviour
{
    private Dropdown dropdown;

    void Start()
    {
        dropdown = GetComponent<Dropdown>();

        dropdown.value = LanguageManager.Instance.CurrentLanguage;
        dropdown.RefreshShownValue();

        dropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    private void OnDropdownChanged(int index)
    {
        LanguageManager.Instance.SetLanguage(index);
    }
}