using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ResourceManager : MonoBehaviour, ISaveable
{
    public enum ResourceType
    {
        Gold,
        Wood,
        Stone,
        Food
    }
    public Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

    void Awake()
    {
        foreach (ResourceType type in System.Enum.GetValues(typeof(ResourceType)))
        {
            resources[type] = 0;
        }
        SaveLoadGame.Instance.Register(this as ISaveable);
    }
    void Oestroy()
    {
        SaveLoadGame.Instance.UnRegister(this as ISaveable);        
    }
    public void SaveData(SaveData data)
    {
        data.gold = resources[ResourceType.Gold];
        data.wood = resources[ResourceType.Wood];
        data.stone = resources[ResourceType.Stone];
        data.food = resources[ResourceType.Food];
    }
    public void LoadData(SaveData data)
    {
        resources[ResourceType.Gold] = data.gold;
        resources[ResourceType.Wood] = data.wood;
        resources[ResourceType.Stone] = data.stone;
        resources[ResourceType.Food] = data.food;
    }
    public void AddGold()
    {
        resources[ResourceType.Gold]++;
        Debug.Log("Gold: " + resources[ResourceType.Gold]);
    }

    //Need delete
    public void ExitGame()
    {
        StartCoroutine(SaveAndExit());
    }
    private IEnumerator SaveAndExit()
    {
        SaveLoadGame.Instance.Save(); // одразу пише
        yield return null; // чекаємо один кадр, щоб гарантовано завершився запис
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
