using System.Collections.Generic;
using UnityEngine;

public class AStarMovement : MonoBehaviour
{
    GameManager _gm { get => GameManager.Instance; }
    AStarPf _pf;
    [SerializeField] float _speed;

    [SerializeField] Node _startNode;
    Node _endNode { get => _gm.pfEndNode; }
    [SerializeField] List<Vector3> _pathToFollow;

    public Node StartNode
    {
        get { return _startNode; }
        set { _startNode = value; }
    }

    public List<Vector3> Path
    {
        get { return _pathToFollow; }
        set { _pathToFollow = value; }
    }

    private void Awake()
    {
        _pf = new AStarPf();
    }

    public void TravelThroughPath()
    {
        if (_pathToFollow == null || _pathToFollow.Count == 0) return;
        Vector3 posTarget = _pathToFollow[0];
        Vector3 dir = posTarget - transform.position;
        if (dir.magnitude < 0.05f)
        {
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

    public void CreatePath()
    {
        _pathToFollow = _pf.AStar(_startNode, _endNode);
    }

    public void CreatePath(Node _start, Node _end)
    {
        _pathToFollow = _pf.AStar(_start, _end);
    }
}
