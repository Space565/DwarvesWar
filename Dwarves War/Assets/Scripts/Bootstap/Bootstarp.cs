using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstarp : MonoBehaviour
{
    void Awake()
    {
        if(SaveLoadGame.Instance == null)
        {
            GameObject SLG = new GameObject("SaveLoadGame");
            SLG.AddComponent<SaveLoadGame>();
            DontDestroyOnLoad(SLG);
            Debug.Log("IsWooooor; BOOTSTRAP");
        }

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        AudioListener listener = GetComponent<AudioListener>();
        if(listener != null)
            listener.enabled = false;
    }
}
