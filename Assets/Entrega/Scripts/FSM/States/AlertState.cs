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

        //Si el nodo final cambia, lo actualiza
        if(agent.pfEndNode != agent._gm.pfEndNode)    
        {
            agent.pfEndNode = agent._gm.pfEndNode;
            agent.CreatePath(agent.GetNearest(agent.transform.position), agent.pfEndNode);
        }

        //Si el player esta en vision, se mueve hacia èl.
        if (agent.InFieldOfView(_playerPos))          
        {
            agent.Move(_playerDir);
            agent._gm.pfEndNode = agent.GetNearest(_playerPos);    //Actualiza el endNode al nodo mas cercano.
            agent._gm.alertPosition = _playerPos;
            agent._pathToFollow?.Clear();

            if ((_playerDir).magnitude < 0.5f) agent._gm.ResetLevel();

            return;
        }

        //Si llega al punto donde se vio al player, cambia a Patrol
        if (_playerAlertDir.magnitude < 0.5f) Transitions();       

        //Si el punto donde se vio al player es a la vista y no se esta haciendo Pf, se mueve hacia èl
        if (agent.InLineOfSight(_playerAlertDir) && agent._pathToFollow.Count == 0)
        {
            agent.Move(_playerAlertDir);
            return;
        }

        //Crea el camino si esta vacio
        if (agent._pathToFollow.Count == 0) agent.CreatePath();

        //Mientras path tenga algo, se mueve por el camino hecho.
        if (agent._pathToFollow.Count > 0)
        {
            agent.TravelThroughPath();
            return;
        }
        Transitions();
    }

    public override void OnExit()
    {
        agent._gm.playerDetected = false;
        agent.lastVisitNode = agent.pfEndNode;
    }

    public override void Transitions()
    {
        fsm.ChangeState(AgentStates.Patrol);
    }
}