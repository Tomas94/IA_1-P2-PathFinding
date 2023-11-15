using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    Agent agent;

    public ChaseState(Agent _agent)
    {
        agent = _agent;
    }

    public override void OnEnter()
    {
        //agent._renderer.material.color = Color.blue;
        //agent._currentState = AgentStates.Chase;
    }

    public override void OnUpdate()
    {
        //agent._currentStamina -= Time.deltaTime;
        //agent.PursuitNearest();
        //agent.KillOtherAgent();
        //Transitions();
    }

    public override void OnExit() { }

    public override void Transitions()
    {
        //if (agent._currentStamina <= 0) fsm.ChangeState(AgentStates.Rest);
        //if (agent._target == null) fsm.ChangeState(AgentStates.Patrol);
    }
}
