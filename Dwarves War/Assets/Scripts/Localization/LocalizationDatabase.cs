using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Localization/Database")]
public class LocalizationDatabase : ScriptableObject
{
    [SerializeField] private List<LocalizationEntry> entries;

    public string GetText(string key, int language)
    {
        foreach (var e in entries)
        {
            if (e.key == key)
                return e.text[language];
        }
        
    Debug.LogError("Ключ локалізації НЕ знайдений: " + key);

    return key;
    }
}