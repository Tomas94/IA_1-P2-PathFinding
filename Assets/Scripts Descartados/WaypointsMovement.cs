using UnityEngine;

public class WaypointsMovement : MonoBehaviour
{
    [SerializeField] Node[] _pathNodes;
    public Node currentNode;
    int _index = 0;

    [SerializeField] float _speed;
    [SerializeField] float _stopDistance;

    private void Start()
    {
        currentNode = _pathNodes[0];
    }

    public void Patrol(Vector3 nodePos)
    {
        var dir = nodePos - transform.position;
        var destiny = transform.position + dir.normalized * _speed *Time.deltaTime;
        transform.right = dir;
        transform.position = destiny;
    }

    public Vector3 GetNodePosition()
    {
        if (currentNode == null) currentNode = _pathNodes[0];
        
        if ((currentNode.transform.position - transform.position).magnitude > _stopDistance) return currentNode.transform.position;
        else currentNode = GetNextWaypoint(_pathNodes);

        return currentNode.transform.position;
    }

    Node GetNextWaypoint(Node[] waypointsArray)
    {
        if (_index == waypointsArray.Length - 1) _index = 0;
        else _index++;

        return waypointsArray[_index];
    }
}
