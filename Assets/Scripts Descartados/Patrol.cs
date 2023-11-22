using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] Node[] _waypoints;
    public Node nextNode;
    public Node lastNode;
    int _index = 0;

    [SerializeField] float _speed;
    [SerializeField] float _stopDistance;

    private void Awake()
    {
        _stopDistance = 0.05f;
    }

    public void Patrolling()
    {
        if(nextNode == null) nextNode = _waypoints[0];
        var dir = nextNode.transform.position - transform.position;
        
        if (Vector3.Distance(nextNode.transform.position, transform.position) > _stopDistance)
        {
            var destiny = transform.position + dir.normalized * _speed * Time.deltaTime;
            transform.right = dir;
            transform.position = destiny;
        }
        else
        {
            lastNode = nextNode;
            nextNode = NextWaypoint();
        }
    }

    Node NextWaypoint()
    {
        if (_index == _waypoints.Length - 1) _index = 0;
        else _index++;

        return _waypoints[_index];
    }
}
