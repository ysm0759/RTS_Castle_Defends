using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{

    private GameObject unitMarker;
    private NavMeshAgent navMeshAgent;
    private UnitState state;
    private Collider[] hit;
    private Enemy enemyUnit;

    public Transform testDestination;


    private Vector3 targetPosition;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        state = new UnitState();
        enemyUnit = GetComponent<Enemy>();
        InvokeRepeating("SearchMyEnemy", 0f, 0.3f);
        InvokeRepeating("Attack", 0f, enemyUnit.unitInfo.attackSpeed);
        InvokeRepeating("SetDestinationCastle", 0f, 2.0f);
    }

  
    private void OnDrawGizmosSelected()
    {

        if (hit != null && hit.Length == 0)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, UnitDataScriptableObject.traceRange);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, UnitDataScriptableObject.traceRange);
        }
    }





    private void SearchMyEnemy()
    {

        IsArrive();

        if (state.IsTraceState(UnitTraceState.TRACE)) //움직이다 적을 찾은 상태
        {
            hit = Physics.OverlapSphere(transform.position, UnitDataScriptableObject.traceRange, LayerMask.GetMask("User"));
            navMeshAgent.isStopped = false;
            if (hit.Length >= 1)
            {
                MoveTo(hit[0].transform.position);
                state.SetTraceState(UnitTraceState.ATTACK_TRACE);
            }
        }
        else if (state.IsTraceState(UnitTraceState.ATTACK_TRACE))
        {
            hit = Physics.OverlapSphere(transform.position, enemyUnit.unitInfo.attackRange, LayerMask.GetMask("User"));
            if (hit.Length >= 1)
            {
                navMeshAgent.isStopped = true;
                state.SetAttackState(UnitAttackState.DO_ATTACK);
            }
            else
            {
                state.SetTraceState(UnitTraceState.TRACE);
                state.SetAttackState(UnitAttackState.NONE_ATTACK);
            }
        }
        else
        {
            state.SetAttackState(UnitAttackState.NONE_ATTACK);
        }
    }



    private void Attack()
    {
        if (state.IsAttackState(UnitAttackState.DO_ATTACK))
        {
            navMeshAgent.isStopped = true;
            foreach (var tmp in hit)
            {
                IDamagable target = tmp.transform.GetComponent<IDamagable>();
                target?.Hit(enemyUnit.unitInfo.damage);
                targetPosition = new Vector3(target.GetTransform().transform.position.x, transform.position.y, target.GetTransform().transform.position.z);
                transform.LookAt(targetPosition);

                return;
            }
        }


    }

    private void IsArrive()
    {
        if (navMeshAgent.velocity.sqrMagnitude <= 0.2f * 0.2f && navMeshAgent.remainingDistance <= 0.5f)
        {
            state.SetTraceState(UnitTraceState.TRACE);
        }
    }

    public void MoveTo(Vector3 end)
    {
        navMeshAgent.SetDestination(end);
    }




    private void SetDestinationCastle()
    {
        if (state.IsTraceState(UnitTraceState.TRACE))
        {
            MoveTo(testDestination.position);
            state.SetMoveState(UnitMoveState.MOVE);
            state.SetTraceState(UnitTraceState.TRACE);
        }

    }


    public void AttackMove()
    {
        navMeshAgent.isStopped = false;
        state.SetMoveState(UnitMoveState.MOVE);
        state.SetTraceState(UnitTraceState.TRACE);
    }

    public void DirectMove()
    {
        navMeshAgent.isStopped = false;
        state.SetMoveState(UnitMoveState.MOVE);
        state.SetTraceState(UnitTraceState.NONE);
    }

}
