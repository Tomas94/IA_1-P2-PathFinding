using System.Collections.Generic;
using UnityEngine;

public class PathfindingAgent2 : MonoBehaviour
{

    [SerializeField] float _speed = 3;
    Pathfinding2 _pf;

    GameManager2 gm { get => GameManager2.instance; }
    Node2 startNode { get => gm.startNode; }
    Node2 endNode { get => gm.endNode; }

    List<Vector3> _pathToFollow = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        _pf = new Pathfinding2();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var node = gm.GetOnCursorObject();
            if (node != null) SetPosition(node.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // StartCoroutine(_pf.PaintAStar(startNode, endNode, SetPath));
            _pathToFollow = _pf.AStar(startNode, endNode);

            if (startNode != null)
            {
                Vector3 pos = startNode.transform.position;
                SetPosition(pos);
            }
        }

        TravelThroughPath();
    }

    void SetPath(List<Vector3> path)
    {
        _pathToFollow = path;
    }

    void TravelThroughPath()
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
        //transform.forward = dir;
        transform.LookAt(transform.position + dir, Vector3.back);
        transform.position += dir.normalized * _speed * Time.deltaTime;
    }

    void SetPosition(Vector3 pos)
    {
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
