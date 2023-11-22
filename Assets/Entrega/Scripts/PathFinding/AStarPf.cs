using System.Collections.Generic;
using UnityEngine;

public class AStarPf
{
    public List<Vector3> AStar(Node start, Node end)
    {
        if (start == null) return default;
        var frontier = new PriorityQueue<Node>();
        frontier.Enqueue(start, 0);
        var cameFrom = new Dictionary<Node, Node>();
        cameFrom.Add(start, null);

        var costSoFar = new Dictionary<Node, int>();
        costSoFar.Add(start, 0);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            if (current == end)
            {
                var path = new List<Vector3>();
                while (current != start)
                {
                    path.Add(current.transform.position);
                    current = cameFrom[current];
                }
                path.Reverse();
                return path;
            }

            foreach (var item in current._neighborNodes)
            {
                int newCost = costSoFar[current] + item._nodeCost;

                if (!costSoFar.ContainsKey(item) || newCost < costSoFar[current])
                {
                    frontier.Enqueue(item, newCost + Heuristic(end.transform.position, item.transform.position));
                    cameFrom[item] = current;
                    costSoFar[item] = newCost;
                }
            }

        }
        return default;
    }

    float Heuristic(Vector3 start, Vector3 end)
    {
        return (start - end).sqrMagnitude;
    }
}
