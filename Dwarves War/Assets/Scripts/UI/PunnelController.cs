using System;
using System.Collections;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Load game button")]
    public Button loadGameButton;
    [Header("New game button")]
    public Button newGameButton;
    [Header("Setting button")]
    public GameObject panel1;
    public GameObject backgroundBlocker;
    public Button openButton;
    public Button closeButton;
    public AnimationClip openAnimation;  
    public AnimationClip closeAnimation; 
    private bool isOpen;
    private Animator animator;
    [Header("Exit Button")]
    public Button exitGame;
    private void Start()
    {
        //Load game button
        loadGameButton.onClick.AddListener(loadGame);
        //New game button
        newGameButton.onClick.AddListener(newGame);
        //Setting button
        animator = panel1.GetComponent<Animator>();
        openButton.onClick.AddListener(OpenPanel);
        closeButton.onClick.AddListener(ClosePanel);
        //Exit Button
        exitGame.onClick.AddListener(ExitGame);
    }

    private void loadGame()
    {
        loadGameButton.interactable = false;
        StartCoroutine(LoadHubAndApplyData());
    }

    private IEnumerator LoadHubAndApplyData()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Hub",LoadSceneMode.Additive); 
        while(!asyncLoad.isDone)
            yield return null;
        if(SaveLoadGame.Instance != null)
            SaveLoadGame.Instance.Load();
        SceneManager.UnloadSceneAsync("MainMenu");
    }
    private void newGame()
    {
        SceneManager.LoadScene("Hub");
    }
    private void OpenPanel()
    {
        panel1.SetActive(true);
        backgroundBlocker.SetActive(true);
        if(animator!=null)
        {
            animator.enabled = true;
        if (openAnimation != null)
                animator.Play(openAnimation.name);
        }
    }

    private void ClosePanel()
{
    if (animator != null)
        {
            animator.enabled = true;
            if (closeAnimation != null)
                animator.Play(closeAnimation.name);
        }

    // відкладене ховання панелі
    StartCoroutine(DisableAfterAnimation(animator));
}

    private System.Collections.IEnumerator DisableAfterAnimation(Animator anim)
{
    // чекаємо поки анімація Close завершиться
    yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
    panel1.SetActive(false);
    backgroundBlocker.SetActive(false);
}
    private void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        SaveLoadGame.Instance.Save();
        #else
        Application.Quit();
        SaveLoadGame.Instance.Save();
        #endif
    }
}