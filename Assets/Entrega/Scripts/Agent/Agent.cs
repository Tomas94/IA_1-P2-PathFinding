using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public GameManager _gm { get => GameManager.Instance; }
    public AgentStates _currentState;
    FiniteStateMachine _fsm;

    public Node[] _patrolNodes;
    int _patrolIndex = 0;

    AStarPf _pf;
    public List<Vector3> _pathToFollow;

    public Node lastVisitNode;
    public Node currentGoingNode;
    public Node pfStartNode;
    public Node pfEndNode;

    [SerializeField] float _speed;
    [SerializeField] float _stopDistance;

    public LayerMask wallLayer;
    [SerializeField] float _viewRadius = 3;
    float _viewAngle = 90;

    private void Awake()
    {
        CreateAndSetFSM();
        _pf = new AStarPf();
        lastVisitNode = _patrolNodes[0];
        transform.position = lastVisitNode.transform.position;
        currentGoingNode = _patrolNodes[1];
    }

    void Update()
    {
        _fsm.Update();
    }

    public bool InFieldOfView(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        if (!InLineOfSight(dir)) return false;
        if (dir.magnitude > _viewRadius) return false;
        return Vector3.Angle(transform.right, dir) <= _viewAngle / 2;
    }

    public bool InLineOfSight(Vector3 dir)
    {
        return !Physics2D.Raycast(transform.position, dir, dir.magnitude, wallLayer);
    }

    public void Move(Vector3 dir)
    {
        transform.right = dir;
        transform.position += dir.normalized * _speed * Time.deltaTime;
    }

    void CreateAndSetFSM()
    {
        _fsm = new FiniteStateMachine();
        _fsm.AddState(AgentStates.Patrol, new PatrolState(this));
        _fsm.AddState(AgentStates.Alert, new AlertState(this));
        _fsm.ChangeState(AgentStates.Patrol);
    }

    public Node GetNearest(Vector3 target)
    {
        Node nearest = null;
        foreach (Node _node in GameManager.Instance._allNodes)
        {
            if (nearest == null)
            {
                nearest = _node;
                continue;
            }

            if (Vector3.Distance(_node.transform.position, target) < Vector3.Distance(nearest.transform.position, target)) nearest = _node;
            
        }
        //Debug.Log("El mas cercano es " + nearest);
        return nearest;
    }

    #region Patrol State

    public void Patrol(Vector3 nodePos)
    {
        var dir = nodePos - transform.position;
        var destiny = transform.position + dir.normalized * _speed * Time.deltaTime;
        transform.right = dir;
        transform.position = destiny;
    }

    public Vector3 GetNodePosition()
    {
        if (currentGoingNode == null) return default;

        if ((currentGoingNode.transform.position - transform.position).magnitude > _stopDistance) return currentGoingNode.transform.position;
        else
        {
            lastVisitNode = currentGoingNode;
            currentGoingNode = GetNextWaypoint(_patrolNodes);
        }
        return currentGoingNode.transform.position;
    }

    Node GetNextWaypoint(Node[] waypointsArray)
    {
        if (_patrolIndex == waypointsArray.Length - 1) _patrolIndex = 0;
        else _patrolIndex++;

        return waypointsArray[_patrolIndex];
    }

    #endregion

    #region Pathfinding State

    public void TravelThroughPath()
    {
        if (_pathToFollow == null || _pathToFollow.Count == 0) return;
        Vector3 posTarget = _pathToFollow[0];
        Vector3 dir = posTarget - transform.position;
        if (dir.magnitude < 0.05f) _pathToFollow.RemoveAt(0);
        Move(dir);
    }

    void SetPosition(Vector3 pos)
    {
        pos.z = transform.position.z;
        transform.position = pos;
    }

    public void CreatePath()
    {
        _pathToFollow = _pf.AStar(pfStartNode, pfEndNode);
    }

    public void CreatePath(Node _start, Node _end)
    {
        _pathToFollow = _pf.AStar(_start, _end);
    }

    #endregion

    #region Gizmos
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
        Vector3 dirA = GetDirFromAngle(_viewAngle / 2);
        Vector3 dirB = GetDirFromAngle(-_viewAngle / 2);

        Gizmos.DrawLine(transform.position, transform.position + dirA.normalized * _viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + dirB.normalized * _viewRadius);
    }
    Vector3 GetDirFromAngle(float angleInDegrees)
    {
        float angle = angleInDegrees + transform.eulerAngles.z;

        return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
    }
    #endregion

}