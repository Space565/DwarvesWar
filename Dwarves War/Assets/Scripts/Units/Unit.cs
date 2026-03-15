using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] protected float _movingSpeed = 10f;
    [SerializeField] protected float _attackRange;
    
    public SkillSet skillSet;
    protected List<SkillState> _skillState = new List<SkillState>();
    protected bool _isAlive = true;
    protected bool _isRunning;
    protected Vector3 _startPos;
    private Vector3 _lastPosition;

    public enum UnitState
    {
        Idle,
        MoveToTarget,
        Attack,
        Return
    }

    protected UnitState _state = UnitState.Idle;
    protected Unit _target;
    protected SkillState  _selectedSkill;

    protected virtual void Awake()
    {
        SetStartPosition();

        //додаєм скіли у свойства скілів
        foreach(var s in skillSet.GetSkills())
        {
            _skillState.Add(new SkillState(s));
        }
    }

    private void Update()
    {
        switch (_state)
        {
            case UnitState.Idle:
                break;

            case UnitState.MoveToTarget:
                MoveToTarget(_target);

                FaceTarget(_target.transform.position);

                if (IsInRange(_target))
                    _state = UnitState.Attack;

                break;

            case UnitState.Attack:
                    _target.TakeDamage(_damage, _selectedSkill);
                    _state = UnitState.Return;

                break;

            case UnitState.Return:
                MoveToStart();

                FaceTarget(_startPos);

                if (IsInStartPosition())
                {
                    FaceTarget(_target.transform.position);
                    _state = UnitState.Idle;
                }
                break;
        }

        UpdateRunningState();
    }
    protected virtual void FaceTarget(Vector3 target)
    {
        Vector3 scale = transform.localScale;

        if (target.x > transform.position.x)
            scale.x = Mathf.Abs(scale.x);
        else
            scale.x = -Mathf.Abs(scale.x);

        transform.localScale = scale;
    }
    protected void StartAttack(Unit target, SkillState skill)
    {
        _selectedSkill = skill;
        _target = target;
        _state = UnitState.MoveToTarget;
    }
    protected virtual void TakeDamage(float damage, SkillState skill)
    {
        _health -= damage + skill.skill.damage;

        if (_health <= 0.01f)
            Die();
    }

    protected virtual void Die()
    {
        _isAlive = false;
        Destroy(gameObject);
    }

    public bool IsAlive()
    {
        return _isAlive;
    }

    public float GetAttackSpeed()
    {
        return _attackSpeed;
    }

    public bool IsInRange(Unit target)
    {
        return Vector2.Distance(transform.position, target.transform.position) <= _attackRange;
    }

    public void MoveToTarget(Unit target)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.transform.position,
            _movingSpeed * Time.deltaTime);
    }

    public void MoveToStart()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            _startPos,
            _movingSpeed * Time.deltaTime);
    }

    public void SetStartPosition()
    {
        _startPos = transform.position;
    }

    protected void UpdateRunningState()
    {
        _isRunning = _state == UnitState.MoveToTarget || _state == UnitState.Return;
    }

    public bool IsInStartPosition()
    {
        float distance = Vector3.Distance(transform.position, _startPos);
        return distance <= 0.05f;
    }

    public bool IsRunning()
    {
        return _isRunning;
    }
    public bool IsIdle()
    {
        return _state == UnitState.Idle;
    }
    public List<SkillState> GetRuntimeSkills()
    {
        return _skillState;
    }
    public bool TryToUseSkill(Unit target, SkillState state)
    {
        if(!state.IsReady())
            return false;
        
        state.Use();
        StartAttack(target, state);
        return true;
    }
    public void TickCooldown()
    {
        foreach(var s in _skillState)
            s.Tick();            
    }
}