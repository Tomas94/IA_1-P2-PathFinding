using UnityEngine;

public class PatrolSystem : MonoBehaviour
{
    [SerializeField] Transform[] _waypoints;
    Vector2 _currentPoint;
    int _index = 0;

    [SerializeField] float _speed;
    [SerializeField] float _stopDistance;

    private void Awake()
    {
        _stopDistance = 0.05f;
    }

    void Update()
    {
        Patrol();
    }

    public void Patrol()
    {
        if(_currentPoint == Vector2.zero) _currentPoint = _waypoints[0].position;
        var dir = _currentPoint - (Vector2)transform.position;
        
        if (Vector2.Distance(_currentPoint, (Vector2)transform.position) > _stopDistance)
        {
            var destiny = (Vector2)transform.position + dir.normalized * _speed * Time.deltaTime;
            transform.right = dir;
            transform.position = destiny;
        }
        else
        {
            _currentPoint = NextWaypoint();
        }
    }

    Vector2 NextWaypoint()
    {
        if (_index == _waypoints.Length - 1) _index = 0;
        else _index++;

        return _waypoints[_index].position; ;
    }
}
