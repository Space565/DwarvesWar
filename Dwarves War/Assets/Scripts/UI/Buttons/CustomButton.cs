using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Animator _animator;

    private readonly int Pressed = Animator.StringToHash("Pressed");
    private readonly int UnPressed = Animator.StringToHash("UnPressed");
    private readonly int Normal = Animator.StringToHash("Normal");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _animator.SetTrigger(Normal);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _animator.ResetTrigger(UnPressed);
        _animator.SetTrigger(Pressed);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _animator.ResetTrigger(Pressed);
        _animator.SetTrigger(UnPressed);
    }
}