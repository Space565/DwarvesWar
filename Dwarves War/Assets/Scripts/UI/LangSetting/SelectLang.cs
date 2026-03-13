using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SelectLang : MonoBehaviour
{
    public int language;
    private TMP_Dropdown dropdown;

    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();

    int savedLanguage = PlayerPrefs.GetInt("language", 0);

    // вимикаємо виклики подій
    dropdown.onValueChanged.RemoveAllListeners();

    dropdown.value = savedLanguage;
    dropdown.RefreshShownValue();

    // додаємо слухача після того, як встановили value
    dropdown.onValueChanged.AddListener(SetLanguage);
    }
    public void SetLanguage(int index)
    {
        if (PlayerPrefs.GetInt("language", -1) == index) 
            return;
        PlayerPrefs.SetInt("language", index);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
