using System.Collections.Generic;
using UnityEngine;

public class Agent2 : MonoBehaviour
{
    [SerializeField] List<GameObject> otherAgents = new List<GameObject>();
    public LayerMask wallLayer;
    [SerializeField] float _viewRadius = 3;
    [SerializeField] float _viewAngle = 90;

    void Update()
    {
        foreach (var item in otherAgents)
        {
            if (InFieldOfView(item.transform.position))
            {
                ChangeColor(item, Color.red);
                Debug.DrawLine(transform.position, item.transform.position, Color.red);
            }
            else
            {
                ChangeColor(item, Color.white);
            }
        }
    }

    bool InFieldOfView(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        if (!InLineOfSight(target)) return false;
        if (dir.magnitude > _viewRadius) return false;
        return Vector3.Angle(transform.forward, dir) <= _viewAngle / 2;
    }

    bool InLineOfSight(Vector3 dir)
    {
        return !Physics.Raycast(transform.position, dir, dir.magnitude, wallLayer);
    }

    void ChangeColor(GameObject obj, Color color)
    {
        obj.GetComponent<Renderer>().material.color = color;
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
        float angle = angleInDegrees + transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

}
