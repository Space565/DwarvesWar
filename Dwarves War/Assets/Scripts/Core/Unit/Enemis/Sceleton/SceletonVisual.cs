using UnityEngine;

public class SceletonVisual : MonoBehaviour
{
    private const string ISRUNNING = "isRunning";    
    private Animator _animator;
    private Enemy _enemy;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponentInParent<Enemy>();
    }
    void Update()
    {
        _animator.SetBool(ISRUNNING, _enemy.IsRunning());
    }
}
