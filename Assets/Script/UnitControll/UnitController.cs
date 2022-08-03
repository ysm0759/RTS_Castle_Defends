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

    bool canAttack = true;

    int hitCount = 0;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        userUnit = GetComponent<UserUnit>();
        anim = GetComponent<Animator>();
        attackType = GetComponent<ObjectAttack>();

        state = new UnitState();
        Debug.Log(userUnit.unitInfo.multiAttack);
        hit = new Collider[userUnit.unitInfo.multiAttack];

        StartCoroutine(SearchTrace());
        SetAnimAttackTime();
    }


    IEnumerator SearchTrace()
    {
        while (true)
        {
            Debug.Log(state.GetAttackState());
            Debug.Log(state.GetTraceState());
            Debug.Log(state.GetMoveState());
            IsArrive();
            if (state.IsMoveState(UnitMoveState.STOP) || KeyManager.instance.skill == Skill.SKILL_USING_CANT_MOVE || KeyManager.instance.skill == Skill.SKILL_USING_CAN_MOVE)
            {
                state.SetAttackState(UnitAttackState.NONE_ATTACK);
            }
            else
            {
                Debug.Log(" ???>??");
                if (state.IsTraceState(UnitTraceState.TRACE))
                {
                    Debug.Log(" ???");
                    if (Physics.OverlapSphereNonAlloc(transform.position, UnitDataScriptableObject.traceRange, hit, LayerMask.GetMask("Enemy")) >= 1)
                    {
                        if (state.IsMoveState(UnitMoveState.MOVE))
                        {
                            MoveTo(hit[0].transform.position);
                        }
                        state.SetTraceState(UnitTraceState.ATTACK_TRACE);
                    }
                }
                if (state.IsTraceState(UnitTraceState.ATTACK_TRACE) && !state.IsMoveState(UnitMoveState.NONE))
                {
                    hitCount = Physics.OverlapSphereNonAlloc(transform.position, userUnit.unitInfo.attackRange, hit, LayerMask.GetMask("Enemy"));
                    Debug.Log(hitCount);
                    if (hitCount >= 1)
                    {
                        navMeshAgent.isStopped = true;
                        state.SetAttackState(UnitAttackState.DO_ATTACK);
                        anim.SetBool("IsMove", false);
                        anim.SetFloat("AttackSpeed", animAttackSpeed);
                        targetPosition = new Vector3(hit[0].transform.position.x, transform.position.y, hit[0].transform.position.z);

                        if (canAttack)
                        {
                            StartCoroutine("Attack");
                        }
                    }
                    else
                    {
                        Debug.Log(" ???>????????????");
                        state.SetTraceState(UnitTraceState.TRACE);
                        state.SetAttackState(UnitAttackState.NONE_ATTACK);
                        anim.SetBool("Attack", false);
                    }
                }
                else
                {
                    Debug.Log(" ???>?!!!!!!!!!!!!!!?");
                    state.SetAttackState(UnitAttackState.NONE_ATTACK);
                    anim.SetBool("Attack", false);
                }
            }
            yield return new WaitForSeconds(0.3f);
        }
    }



    IEnumerator Attack()
    {
        canAttack = false;
        if (state.IsMoveState(UnitMoveState.STOP))
        {
            anim.SetBool("Attack", false);
        }
        else if (state.IsAttackState(UnitAttackState.DO_ATTACK))
        {
            transform.LookAt(targetPosition);
            anim.SetBool("Attack", true);

            if (attackType != null)
            {
                for (int i = 0; i < hitCount; i++)
                {
                    attackType?.Attack(hit[i], userUnit.unitInfo.damage, userUnit.unitInfo.attackName);
                }
            }
            else
                Debug.Log("데이터 잘 못 넣음 확인바람!!!!!!!!!!!!!!!!!");

        }
        yield return new WaitForSeconds(userUnit.unitInfo.attackSpeed);
        canAttack = true;
    }


    public void SetAnimAttackTime()
    {
        RuntimeAnimatorController ac = anim.runtimeAnimatorController;
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
        anim.SetBool("IsMove", true);

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
