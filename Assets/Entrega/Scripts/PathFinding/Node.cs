using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> _neighborNodes = new List<Node>();
    [SerializeField] LayerMask _wallLayer;

    [HideInInspector] public int _nodeCost = 1;

    void Start()
    {
        GetNeighbors();
    }

    //Consigue los vecinos
    public void GetNeighbors()
    {
        foreach (var node in GameManager.Instance._allNodes)
        {
            if (node == this) continue;

            if (InLineOfSight(node) && !_neighborNodes.Contains(node))
            {
                _neighborNodes.Add(node);
                node._neighborNodes.Add(this);
            }

        }
    }

    bool InLineOfSight(Node node)
    {
        Vector2 dir = node.transform.position - transform.position;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, dir.magnitude, _wallLayer); //Raycast para verificar que el nodo esta en visi�n.
        if (hits[1].collider?.GetComponent<Node>() == node) return true;                                            //Indice 1 porque el 0 es el mismo objeto que dispara el rayo.
        return false;
    }

    //Devuelve la posicion del nodo
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //Dibujo de grafo con gizmos
        foreach (Node node in _neighborNodes) Gizmos.DrawLine(transform.position, node.transform.position); 
    }
}
