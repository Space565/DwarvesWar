using System;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;

    public int CurrentLanguage { get; private set; }

    public static event Action OnLanguageChanged;

    private void Awake()
    {
        Instance = this;
        CurrentLanguage = PlayerPrefs.GetInt("language", CurrentLanguage);
    }

    public void SetLanguage(int index)
    {
        CurrentLanguage = index;
        PlayerPrefs.SetInt("language", index);
        PlayerPrefs.Save();

        OnLanguageChanged?.Invoke(); // повідомляємо всі тексти
    }
}