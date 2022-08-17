using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UnitMoveState
{
    STOP,
    MOVE,
    HOLD,
    NONE
}

public enum UnitTraceState
{
    TRACE,
    ATTACK_TRACE,
    ATTACK_FOCUS,
    NONE
}
public enum UnitAttackState
{
    DO_ATTACK,
    NONE_ATTACK,
}

public class UnitState
{

    UnitMoveState moveState;
    UnitTraceState traceState;
    UnitAttackState attackState;
    public UnitState()
    {
        moveState = UnitMoveState.STOP;
        traceState = UnitTraceState.TRACE;
        attackState = UnitAttackState.NONE_ATTACK;
    }
    public bool IsMoveState(UnitMoveState _state)
    {
        if (this.moveState == _state)
            return true;
        return false;
    }

    public void SetMoveState(UnitMoveState _state)
    {
        this.moveState = _state;
    }

    public UnitMoveState GetMoveState()
    {
        return moveState;
    }


    public bool IsTraceState(UnitTraceState _state)
    {
        if (this.traceState == _state)
            return true;
        return false;
    }

    public void SetTraceState(UnitTraceState _state)
    {
        this.traceState = _state;
    }

    public UnitTraceState GetTraceState()
    {
        return traceState;
    }


    
    public bool IsAttackState(UnitAttackState _state)
    {
        if (this.attackState == _state)
            return true;
        return false;
    }

    public void SetAttackState(UnitAttackState _state)
    {
        this.attackState = _state;
    }

    public UnitAttackState GetAttackState()
    {
        return attackState;
    }
}
