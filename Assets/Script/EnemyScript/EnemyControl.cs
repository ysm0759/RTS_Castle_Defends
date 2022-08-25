using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;
    private UnitState state;
    private Collider[] hit;
    private Enemy enemyUnit;


    private Destination dest;
    private Vector3 destination;
    private Collider destinationCollider;
    List<Transform> node;
    int nodeIndex = 0;

    private Animator anim;
    private float animAttackTime;
    private float animAttackSpeed;
    private ObjectAttack attackType;

    bool canAttack = true;
    private Vector3 targetPosition;
    int hitCount = 0;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyUnit = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
        attackType = GetComponent<ObjectAttack>();
        dest = FindObjectOfType<Destination>();
    }

    
    private void OnEnable()
    {

    }


    void Start()
    {
        navMeshAgent.enabled = true;
        navMeshAgent.speed = enemyUnit.unitInfo.moveSpeed;
        MoveTypeOnlyDestination();
        SetDestinationRandom();
        state = new UnitState();
        hit = new Collider[enemyUnit.unitInfo.multiAttack];
        StartCoroutine(SetMultiAttackSize());
        SetAnimAttackTime();
        Debug.Log("start");

    }


    public void SetNodeIndex(int idx)
    {
        nodeIndex = idx;
    }

    public int GetNodeIndex()
    {
        return nodeIndex;
    }
    public void MoveTypeDense()
    {
        destinationCollider = dest.GetComponent<Collider>();
        destination = dest.transform.position;
    }

    public void MoveTypeOnlyDestination()
    {
        destinationCollider = dest.GetComponent<Collider>();
        destination = dest.NodeTypeLeft[nodeIndex].transform.position;
        node = dest.NodeTypeLeft;
    }


    IEnumerator SetMultiAttackSize()
    {
        yield return null;
        hit = new Collider[enemyUnit.unitInfo.multiAttack];
        StartCoroutine(SearchTrace());
    }

    IEnumerator SearchTrace()
    {
        //NavMeshPath navMeshPath = new NavMeshPath();
        //navMeshAgent.CalculatePath(destination, navMeshPath);
        navMeshAgent.destination = destination;
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(2.0f);
        navMeshAgent.isStopped = false;
        StartCoroutine(SetDestinationCastle());

        while (true)
        {
            Debug.Log("trace");

            if (!enemyUnit.unitInfo.isAlive)
            {
                navMeshAgent.isStopped = true;
                anim.SetTrigger("OnDead");
                yield break;
            } 

            IsArrive();


            if (state.IsTraceState(UnitTraceState.TRACE)) //움직이다 적을 찾은 상태
            {
                if (Physics.OverlapSphereNonAlloc(transform.position, UnitDataScriptableObject.traceRange, hit, LayerMask.GetMask("User")) >= 1)
                {
                    MoveTo(hit[0].transform.position);
                    state.SetTraceState(UnitTraceState.ATTACK_TRACE);
                }
            }
            if (state.IsTraceState(UnitTraceState.ATTACK_TRACE))
            {
                hitCount = Physics.OverlapSphereNonAlloc(transform.position, enemyUnit.unitInfo.attackRange, hit, LayerMask.GetMask("User"));
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
                    state.SetTraceState(UnitTraceState.TRACE);
                    state.SetAttackState(UnitAttackState.NONE_ATTACK);
                    anim.SetBool("Attack", false);
                }
            }
            else
            {
                state.SetAttackState(UnitAttackState.NONE_ATTACK);
                anim.SetBool("Attack", false);
                //anim.SetBool("IsMove", false);
            }

            yield return new WaitForSeconds(0.3f);
        }

    }

    IEnumerator Attack()
    {
        canAttack = false;

        if (state.IsAttackState(UnitAttackState.DO_ATTACK))
        {
            transform.LookAt(targetPosition);
            anim.SetBool("Attack", true);

            if (attackType != null)
            {
                for (int i = 0; i < hitCount; i++)
                {
                    attackType?.Attack(hit[i], enemyUnit.unitInfo.damage, enemyUnit.unitInfo.attackName);
                }
            }
            else
                Debug.Log("데이터 잘 못 넣음 확인바람!!!!!!!!!!!!!!!!!");

        }
        yield return new WaitForSeconds(enemyUnit.unitInfo.attackSpeed);
        canAttack = true;
    }



    private void IsArrive()
    {
        if (nodeIndex != node.Count)
        {

            Vector3 dir = destination - gameObject.transform.position;
            if (dir.sqrMagnitude <= 300)
            {
                destination = node[nodeIndex].gameObject.transform.position;
                SetDestinationRandom();
                ++nodeIndex;
                if (nodeIndex == node.Count)
                {
                    destination = dest.transform.position;
                }
                MoveTo(destination);
            }
        }
        if (navMeshAgent.velocity.sqrMagnitude <= 0.2f * 0.2f && navMeshAgent.remainingDistance <= 0.5f)
        {
            
            anim.SetBool("IsMove", false);
            state.SetTraceState(UnitTraceState.TRACE);

        }
    }

    public void MoveTo(Vector3 end)
    {
        Debug.Log("moveto");

        if (!enemyUnit.unitInfo.isAlive)
        {
            return;
        }
        navMeshAgent.isStopped = false;
        anim.SetBool("IsMove", true);
        navMeshAgent.SetDestination(end);
    }




    IEnumerator SetDestinationCastle()
    {
        while (true)
        {
            Debug.Log("destk");

            if (!enemyUnit.unitInfo.isAlive)
            {
                yield break;
            }
            if (state.IsTraceState(UnitTraceState.TRACE) && !state.IsAttackState(UnitAttackState.DO_ATTACK))
            {
                MoveTo(destination);
                state.SetMoveState(UnitMoveState.MOVE);
                state.SetTraceState(UnitTraceState.TRACE);
            }
            yield return new WaitForSeconds(2.0f);
        }
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
        animAttackSpeed = animAttackTime / enemyUnit.unitInfo.attackSpeed;

    }


    private void SetDestinationRandom()
    {
        
        Vector3 originPosition = destination;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = destinationCollider.bounds.size.x;
        float range_Z = destinationCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);
        Vector3 respawnPosition = originPosition + RandomPostion;

        destination = respawnPosition;
    }


}
