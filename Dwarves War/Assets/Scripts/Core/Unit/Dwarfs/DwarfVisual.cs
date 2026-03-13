using UnityEngine;

public class DwarfVisual : MonoBehaviour
{
    private const string ISRUNNING = "isRunning";    
    private Animator _animator;
    private Dwarf _dwarf;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _dwarf = GetComponentInParent<Dwarf>();
    }
    void Update()
    {
        _animator.SetBool(ISRUNNING, _dwarf.IsRunning());
    }

}
