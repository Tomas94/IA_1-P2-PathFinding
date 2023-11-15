using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    Agent agent;

    public PatrolState(Agent _agent)
    {
        agent = _agent;
    }

    public override void OnEnter()
    {
        //agent._renderer.material.color = Color.green;
        //agent._currentState = AgentStates.Patrol;
    }

    public override void OnUpdate()
    {
        //agent.Patrol();
        //agent.CheckForBoidInRange();
        //agent._currentStamina -= Time.deltaTime;
        //Transitions();
    }

    public override void OnExit()
    {
        //throw new System.NotImplementedException();
    }

    public override void Transitions()
    {
        //if (agent._currentStamina <= 0) fsm.ChangeState(AgentStates.Rest);
        //if (agent._target != null) fsm.ChangeState(AgentStates.Chase);
    }
}
