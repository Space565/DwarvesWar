using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
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
        //New game button
        newGameButton.onClick.AddListener(newGame);
        //Setting button
        animator = panel1.GetComponent<Animator>();
        openButton.onClick.AddListener(OpenPanel);
        closeButton.onClick.AddListener(ClosePanel);
        //Exit Button
        exitGame.onClick.AddListener(ExitGame);
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
        #else
        Application.Quit();
        #endif
    }
}