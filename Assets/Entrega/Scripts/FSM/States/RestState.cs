using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestState : State
{
    Agent agent;

    public RestState(Agent _agent)
    {
        agent = _agent;
    }

    public override void OnEnter()
    {
        //agent._renderer.material.color = Color.grey;
        //agent._currentState = AgentStates.Rest;
    }

    public override void OnUpdate()
    {
        //agent.RefreshStamina();
        //if (agent._currentStamina >= agent._maxStamina) Transitions();

    }

    public override void OnExit()
    {
        //throw new System.NotImplementedException();
    }

    public override void Transitions()
    {
        //if (agent?._target) fsm.ChangeState(AgentStates.Chase);
        //else fsm.ChangeState(AgentStates.Patrol);
    }
}
