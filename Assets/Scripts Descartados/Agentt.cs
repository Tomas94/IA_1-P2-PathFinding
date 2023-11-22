using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agentt : MonoBehaviour
{
    public AgentStates _currentState;
    FiniteStateMachine _fSM;
    AStarPf _pf;

    //AStarMovement _aStarPathfinding;
    //WaypointsMovement _waypointsMovement;

    GameManager gm { get => GameManager.Instance; }
    Node endNode { get => gm.pfEndNode; }
    public Node _startNode;

    public Patrol patrol;
    public List<Vector3> _pathToFollow = new List<Vector3>();
    
    public LayerMask wallLayer;
    [SerializeField] float _speed;
    float _viewRadius = 3;
    float _viewAngle = 90;

    private void Awake()
    {
        //CreateFSM();
        _pf = new AStarPf();
        patrol = GetComponent<Patrol>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //_pathToFollow = _pf.AStar(_startNode, endNode);
            _fSM.ChangeState(AgentStates.Alert);

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //_pathToFollow = _pf.AStar(_startNode, endNode);
            _fSM.ChangeState(AgentStates.Patrol);

        }
        TravelThroughPath();
        _fSM.Update();
    }

    public void TravelThroughPath()
    {
        if (_pathToFollow == null || _pathToFollow.Count == 0) return;
        Vector3 posTarget = _pathToFollow[0];
        posTarget.z = transform.position.z;
        Vector3 dir = posTarget - transform.position;
        if (dir.magnitude < 0.05f)
        {
            SetPosition(posTarget);
            _pathToFollow.RemoveAt(0);
        }

        Move(dir);
    }
    void Move(Vector3 dir)
    {
        transform.right = dir;
        transform.position += dir.normalized * _speed * Time.deltaTime;
    }
    void SetPosition(Vector3 pos)
    {
        pos.z = transform.position.z;
        transform.position = pos;
    }


    public bool InFieldOfView(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        if (!InLineOfSight(target)) return false;
        if (dir.magnitude > _viewRadius) return false;
        return Vector3.Angle(transform.right, dir) <= _viewAngle / 2;
    }

    public bool InLineOfSight(Vector3 dir)
    {
        return !Physics.Raycast(transform.position, dir, dir.magnitude, wallLayer);
    }


    public Node GetNearest(Vector3 target)
    {
        Node nearest = null;
        foreach(var item in gm._allNodes)
        {
            if(nearest == null) nearest = item;
            if(Vector3.Distance(target,item.transform.position) < Vector3.Distance(target, nearest.transform.position))
            {
                nearest = item;
            }
        }
        return nearest;
    }

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
        return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad),0);
    }
    
   /* void CreateFSM()
    {
        _fSM = new FiniteStateMachine();
        _fSM.AddState(AgentStates.Patrol, new PatrolState(this));
        _fSM.AddState(AgentStates.Alert, new AlertState(this));
        _fSM.ChangeState(AgentStates.Patrol);
    }
*/}