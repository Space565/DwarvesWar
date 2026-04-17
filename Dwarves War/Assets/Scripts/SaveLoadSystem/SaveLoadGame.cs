using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadGame : MonoBehaviour
{
    public static SaveLoadGame Instance;
    private List<ISaveable> _saveables = new List<ISaveable>();
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void Register(ISaveable s)
    {
        if(!_saveables.Contains(s))
            _saveables.Add(s);
    }
    public void UnRegister(ISaveable s)
    {
        if(_saveables.Contains(s))
            _saveables.Remove(s);
    }

    public void Save()
    {
        SaveData data = new SaveData();
        string path = Application.persistentDataPath + "/save.json";

        foreach(var s in _saveables)
            s.SaveData(data);

        string json = JsonUtility.ToJson(data, true);
        
        System.IO.File.WriteAllText(path,json);
        Debug.Log("Save path: " + path);
    }
    public void Load()
    {
        Debug.Log("Is workbl"); 

        string path = Application.persistentDataPath + "/save.json";

        if (!File.Exists(path))
        {
            Debug.LogWarning("Файл збереження не знайдено!");
            return;
        }
        string json = File.ReadAllText(path);

        SaveData data = JsonUtility.FromJson<SaveData>(json);

        foreach(var s in _saveables)
            s.LoadData(data);
    }
}
