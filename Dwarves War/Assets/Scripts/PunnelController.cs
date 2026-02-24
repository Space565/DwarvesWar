using System;
using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Setting button")]
    public GameObject panel1;
    public GameObject backgroundBlocker;
    public Button openButton;
    public Button closeButton;
    public AnimationClip openAnimation;  
    public AnimationClip closeAnimation; 
    private bool isOpen;
    private Animator animator;
    //public Animator animationClose;
    [Header("Exit Button")]
    public Button exitGame;
    private void Start()
    {
        animator = panel1.GetComponent<Animator>();
        openButton.onClick.AddListener(OpenPanel);
        closeButton.onClick.AddListener(ClosePanel);
        exitGame.onClick.AddListener(ExitGame);
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