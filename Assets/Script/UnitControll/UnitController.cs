using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    [SerializeField]
    private GameObject unitMarker;
    private NavMeshAgent navMeshAgent;
    private UnitState state;
    private Collider[] hit;
    private UserUnit userUnit;

    private Animator anim;
    private float animAttackTime;
    private float animAttackSpeed;

    private ObjectAttack attackType;

    Vector3 targetPosition;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        state = new UnitState();
        userUnit = GetComponent<UserUnit>();
        InvokeRepeating("SearchTrace", 0f, 0.3f);
        //InvokeRepeating("IsArrive", 0f, 0.2f);
        InvokeRepeating("Attack", 0f, userUnit.unitInfo.attackSpeed);
        anim = GetComponent<Animator>();
        SetAnimAttackTime();
        attackType = GetComponent<ObjectAttack>();
        hit = new Collider[5];
    }


    private void Update()
    {
        IsArrive();
    }

    private void SearchTrace()
    {

        if (state.IsMoveState(UnitMoveState.STOP) || KeyManager.instance.skill == Skill.SKILL_USING_CANT_MOVE || KeyManager.instance.skill == Skill.SKILL_USING_CAN_MOVE)
        {
            state.SetAttackState(UnitAttackState.NONE_ATTACK);
            return;
        }

        Debug.Log(state.GetTraceState());
        Debug.Log(state.GetMoveState());
        if (state.IsTraceState(UnitTraceState.TRACE))
        {
            if (Physics.OverlapSphereNonAlloc(transform.position, UnitDataScriptableObject.traceRange, hit, LayerMask.GetMask("Enemy")) >= 1)
            {
                Debug.Log("Search");
                if(state.IsMoveState(UnitMoveState.MOVE))
                {
                    MoveTo(hit[0].transform.position);
                }
                state.SetTraceState(UnitTraceState.ATTACK_TRACE);
            }
        }
        if (state.IsTraceState(UnitTraceState.ATTACK_TRACE) && !state.IsMoveState(UnitMoveState.NONE))
        {
            
            if (Physics.OverlapSphereNonAlloc(transform.position, userUnit.unitInfo.attackRange, hit, LayerMask.GetMask("Enemy")) >= 1)
            {
                navMeshAgent.isStopped = true;
                state.SetAttackState(UnitAttackState.DO_ATTACK);
                anim.SetBool("Attack", true);
                anim.SetBool("IsMove", false);
                Debug.Log("Do_ATTACK");
                anim.SetFloat("AttackSpeed", animAttackSpeed);
            }
            else
            {
                state.SetTraceState(UnitTraceState.TRACE);
                state.SetAttackState(UnitAttackState.NONE_ATTACK);
                anim.SetBool("Attack", false);

            }
        }
        else
        {
            state.SetAttackState(UnitAttackState.NONE_ATTACK);
            anim.SetBool("Attack", false);
        }
    }



    private void Attack()
    {
        if (state.IsMoveState(UnitMoveState.STOP))
        {
            anim.SetBool("Attack", false);
            return;
        }

        if (state.IsAttackState(UnitAttackState.DO_ATTACK))
        {

            foreach (var tmp in hit)
            {
                IDamagable target = tmp.transform.GetComponent<IDamagable>();
                target?.Hit(userUnit.unitInfo.damage);
                targetPosition = new Vector3(target.GetTransform().transform.position.x, transform.position.y, target.GetTransform().transform.position.z);
                transform.LookAt(targetPosition);
                if(attackType != null)
                {
                    attackType?.Attack(hit);

                }
                else
                {
                    Debug.Log("데이터 잘 못 넣음 확인바람!!!!!!!!!!!!!!!!!");
                }
                return;
            }
        }


    }


    public void SetAnimAttackTime()
    {
        RuntimeAnimatorController ac = anim.runtimeAnimatorController;
        Debug.Log(ac.animationClips.Length);
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name.ToUpper().Contains("ATTACK"))
            {
                animAttackTime = ac.animationClips[i].length;
                break;
            }
        }
        SetAnimAttackSpeed();

    }

    private void SetAnimAttackSpeed()
    {
        animAttackSpeed = animAttackTime / userUnit.unitInfo.attackSpeed;

    }
    public void SelectUnit()
    {
        unitMarker.SetActive(true);
    }


    public void DeselectUnit()
    {
        unitMarker.SetActive(false);
    }

    public bool isSelected()
    {
        return unitMarker.activeInHierarchy;
    }
    public void MoveTo(Vector3 end)
    {

        if (CompareTag("Hero") && KeyManager.instance.skill == Skill.SKILL_USING_CANT_MOVE)
        {
            return;
        }
        anim.SetBool("IsMove", true);
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(end);
    }


    public void Hold()
    {
        anim.SetBool("IsMove", false);

        navMeshAgent.isStopped = true;
        state.SetMoveState(UnitMoveState.HOLD);
        state.SetTraceState(UnitTraceState.ATTACK_TRACE);

    }

    public void AttackMove()
    {
        anim.SetBool("IsMove",true);

        navMeshAgent.isStopped = false;
        state.SetMoveState(UnitMoveState.MOVE);
        state.SetTraceState(UnitTraceState.TRACE);
    }

    public void DirectMove()
    {
        anim.SetBool("IsMove", true);
        navMeshAgent.isStopped = false;
        state.SetMoveState(UnitMoveState.MOVE);
        state.SetTraceState(UnitTraceState.NONE); //TODO
        Debug.Log("너가 왜 들어가 ?!!!");
    }

    private void IsArrive()
    {
        //if(navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
        //if(navMeshAgent.velocity == Vector3.zero)
  
        if (navMeshAgent.velocity.sqrMagnitude <= 0.2f * 0.2f && navMeshAgent.remainingDistance <= 0.5f
            && navMeshAgent.velocity == Vector3.zero)
        {
            anim.SetBool("IsMove", false);
            state.SetTraceState(UnitTraceState.TRACE);
            Debug.Log("도착");
        }
    }


    public void SetNavStop()
    {
        navMeshAgent.ResetPath();
    }
    public void Stop()
    {
        navMeshAgent.isStopped = true;
        anim.SetBool("IsMove", false);
        state.SetMoveState(UnitMoveState.STOP);
        state.SetTraceState(UnitTraceState.NONE);
    }
}
