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
        agent._currentState = AgentStates.Patrol;
    }

    public override void OnUpdate()
    {
        Transitions();

        var _playerPos = agent._gm.player.transform.position;
        var _currenNodePos = agent.currentGoingNode.GetPosition();
        var _nodeDir = _currenNodePos - agent.transform.position;

        //Si ve al player, activa el bool que indica que el player fue encontrado y asi se avisa a los demas.
        if (agent.InFieldOfView(_playerPos))
        {
            agent._gm.pfEndNode = agent.currentGoingNode;
            agent._gm.alertPosition = _playerPos;
            agent._gm.playerDetected = true;
            return;
        }

        //Si el proximo nodo no esta a la vista o el path tiene elemtos, recorre el path con A*
        if (!agent.InLineOfSight(_nodeDir) || agent._pathToFollow.Count > 0)
        {
            if (agent._pathToFollow.Count == 0) agent.CreatePath(agent.lastVisitNode, agent.currentGoingNode);
            agent.TravelThroughPath();
            return;
        }

        //Si path esta vacio, patrulla hacia el nodo.
        if (agent._pathToFollow.Count == 0 )
        {
            agent.Patrol(agent.GetNodePosition());
            return;
        }
    }

    public override void OnExit()
    {

    }
    public override void Transitions()
    {
        if (agent._gm.playerDetected) fsm.ChangeState(AgentStates.Alert);
    }
}
