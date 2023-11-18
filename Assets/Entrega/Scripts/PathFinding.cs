using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public List<Vector2> BFS(Node start, Node end)
    {
        Queue<Node> frontier = new Queue<Node>();
        frontier.Enqueue(start);
        var cameFrom = new Dictionary<Node, Node>();  //El primer valor es el nodo actual, el segundo de cual se vino.
        cameFrom.Add(start, null);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            if (current == end)
            {
                var path = new List<Vector2>();
                while (current != start)
                {
                    path.Add(current.transform.position);
                    current = cameFrom[current];
                }
                //path.append(start) # optional
                path.Reverse();
                return path;
            }

            foreach (var item in current._neighborNodes)
            {
                if (!cameFrom.ContainsKey(item))
                {
                    frontier.Enqueue(item);
                    cameFrom.Add(item, current);
                }
            }
        }
        return default;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
