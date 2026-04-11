using UnityEngine;

public class SkeletonVisual : MonoBehaviour, ITappable
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
    public void OnTap()
    {
        BattleManager.Instance.TrySelectTarget(_enemy);
        Debug.Log("Skeleton taped");
    }
}
