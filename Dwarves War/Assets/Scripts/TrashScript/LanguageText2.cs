using UnityEngine;
using TMPro;

public class LanguageText2 : MonoBehaviour
{
    public string[] text;
    private TMP_Text textline;

    void Start()
    {
        textline = GetComponent<TMP_Text>();
        UpdateText();

        LanguageManager.OnLanguageChanged += UpdateText;
    }

    void UpdateText()
    {
        int lang = LanguageManager.Instance.CurrentLanguage;
        textline.text = "" + text[lang];
    }

    private void OnDestroy()
    {
        LanguageManager.OnLanguageChanged -= UpdateText;
    }
}