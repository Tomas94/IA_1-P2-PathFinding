using UnityEngine;

public class AlertState : State
{
    Agent agent;

    public AlertState(Agent _agent)
    {
        agent = _agent;
    }

    public override void OnEnter()
    {
        agent._currentState = AgentStates.Alert;
        agent.pfStartNode = agent.currentGoingNode;
        agent.pfEndNode = agent._gm.pfEndNode;
    }

    public override void OnUpdate()
    {
        var _playerPos = agent._gm.player.transform.position;
        var _playerDir = _playerPos - agent.transform.position;
        var _playerAlertDir = agent._gm.alertPosition - agent.transform.position;

        if (agent.InFieldOfView(_playerPos))
        {
            agent.Move(_playerDir);
            agent._gm.pfEndNode = agent.currentGoingNode;
            agent._gm.alertPosition = _playerPos;
            agent._pathToFollow?.Clear();

            if ((_playerDir).magnitude < 0.5f) agent._gm.ResetLevel();

            return;
        }

        if (_playerAlertDir.magnitude < 0.5f) Transitions();

        if (agent.InLineOfSight(_playerAlertDir) && agent._pathToFollow.Count == 0)
        {
            agent.Move(_playerAlertDir);
            return;
        }

        if (agent._pathToFollow.Count == 0) agent.CreatePath();

        if (agent._pathToFollow.Count > 0)
        {
            agent.TravelThroughPath();
            return;
        }
    }

    public override void OnExit()
    {
        agent._gm.playerDetected = false;
        agent.lastVisitNode = agent.pfEndNode;
        //agent.pfStartNode = agent._gm.pfEndNode;
        //agent.pfEndNode = agent.currentGoingNode;
    }

    public override void Transitions()
    {
        fsm.ChangeState(AgentStates.Patrol);
    }
}