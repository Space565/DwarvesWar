using UnityEngine;
using UnityEngine.UI;

public class HubUIManager : MonoBehaviour
{
    public HubUIManager Instance;
    //Map 
    public GameObject Map;
    private string MapIsOpen = "isOpen";
    //
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void MapOpen()
    {
        
    }

}
