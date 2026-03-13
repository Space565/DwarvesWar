using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LanguageText : MonoBehaviour
{
    public int language;
    public string[] text;
    private TMP_Text textline;
    void Start()
    {
        language = PlayerPrefs.GetInt("language"); //загружаем язык
        textline = GetComponent<TMP_Text>(); //обращаемся к компоненту
        textline.text = "" + text[language]; //указываем какой текст будет отображаться, в зависимости отвыбранного языка
    }

}
