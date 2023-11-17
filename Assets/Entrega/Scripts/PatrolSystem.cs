using UnityEngine;

public class PatrolSystem : MonoBehaviour
{
    [SerializeField] Transform[] _waypoints;
     int _index = 0;
    Vector2 _currentPoint;
    [SerializeField] float _speed;
    void Update()
    {
        Patrol();
    }
    public void Patrol()
    {
        if(_currentPoint == Vector2.zero) _currentPoint = _waypoints[0].position;
        Vector2 dir = _currentPoint - (Vector2)transform.position;
        
        if (Vector2.Distance(_currentPoint, (Vector2)transform.position) > .5f) //Vector2.Distance(_currentPoint, (Vector2) transform.position
        {
            Vector3 destiny = (Vector2)transform.position + dir.normalized * _speed * Time.deltaTime;
            transform.right = dir;
            transform.position = destiny;
            print("dentor del primer if");
        }
        else
        {
            _currentPoint = NextWaypoint();
            print("en el else");
        }
    }

    Vector2 NextWaypoint()
    {
        print("Se ejecutó la funcion");
        if (_index == _waypoints.Length - 1) _index = 0;
        else _index++;

        return _waypoints[_index].position; ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("choque algo");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Colisione con algo");
    }
}
