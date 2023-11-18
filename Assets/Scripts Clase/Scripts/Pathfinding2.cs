using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinding2
{
    /*IGraph _graph;

     public Pathfinding(IGraph graph)
     {
         _graph = graph;
     }*/

    //Breadth First Search
    public List<Vector3> BFS(Node2 start, Node2 end)
    {
        Queue<Node2> frontier = new Queue<Node2>();
        frontier.Enqueue(start);
        var cameFrom = new Dictionary<Node2, Node2>();
        cameFrom.Add(start, null);

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
                //path.append(start) # optional
                path.Reverse(); // optional
                return path;
            }

            foreach (var item in current.Neighbors)
            {
                //if (item.layer == 6) continue; //IsBlocked ?
                if (item.isBlocked) continue;
                //Modificar a futuro para que permita diferentes maneras de chequear por un nodo bloqueado

                if (!cameFrom.ContainsKey(item))
                {
                    frontier.Enqueue(item);
                    cameFrom.Add(item, current);
                }
            }

        }
        return default;
    }

    public List<Vector3> Dijkstra(Node2 start, Node2 end)
    {
        if (start == null) return default;
        //Queue<Node> frontier = new Queue<Node>();
        var frontier = new PriorityQueue<Node2>();
        frontier.Enqueue(start, 0);
        var cameFrom = new Dictionary<Node2, Node2>();
        cameFrom.Add(start, null);

        var costSoFar = new Dictionary<Node2, int>();
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
                //path.append(start) # optional
                path.Reverse(); // optional
                return path;
            }

            foreach (var item in current.Neighbors)
            {
                if (item.isBlocked) continue;

                int newCost = costSoFar[current] + item.Cost;

                if (!costSoFar.ContainsKey(item) || newCost < costSoFar[current])
                {
                    frontier.Enqueue(item, newCost);
                    cameFrom[item] = current;
                    costSoFar[item] = newCost;
                }
            }

        }
        return default;
    }

    public List<Vector3> GreedyBFS(Node2 start, Node2 end)
    {
        PriorityQueue<Node2> frontier = new PriorityQueue<Node2>();
        frontier.Enqueue(start, 0);
        var cameFrom = new Dictionary<Node2, Node2>();
        cameFrom.Add(start, null);

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
                //path.append(start) # optional
                path.Reverse(); // optional
                return path;
            }

            foreach (var item in current.Neighbors)
            {
                //if (item.layer == 6) continue; //IsBlocked ?
                if (item.isBlocked) continue;
                //Modificar a futuro para que permita diferentes maneras de chequear por un nodo bloqueado

                if (!cameFrom.ContainsKey(item))
                {
                    frontier.Enqueue(item, HeuristicManhattan(end.transform.position, item.transform.position));
                    cameFrom.Add(item, current);
                }
            }

        }
        return default;
    }

    public List<Vector3> AStar(Node2 start, Node2 end)
    {
        if (start == null) return default;
        //Queue<Node> frontier = new Queue<Node>();
        var frontier = new PriorityQueue<Node2>();
        frontier.Enqueue(start, 0);
        var cameFrom = new Dictionary<Node2, Node2>();
        cameFrom.Add(start, null);

        var costSoFar = new Dictionary<Node2, int>();
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
                //path.append(start) # optional
                path.Reverse(); // optional
                return path;
            }

            foreach (var item in current.Neighbors)
            {
                if (item.isBlocked) continue;

                int newCost = costSoFar[current] + item.Cost;

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
        return Vector3.Distance(start, end);
    }

    float HeuristicManhattan(Vector3 a, Vector3 b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    float Heuristic2(Vector3 start, Vector3 end)
    {
        return (start - end).sqrMagnitude;
    }


    /*public IEnumerator PaintBFS(GameObject start, GameObject end)
    {
        Queue<GameObject> frontier = new Queue<GameObject>();
        frontier.Enqueue(start);
        var cameFrom = new Dictionary<GameObject, GameObject>();
        cameFrom.Add(start, null);
        GameObject current = null;
        while (frontier.Count > 0)
        {
            current = frontier.Dequeue();
            ChangeObjectColor(current, Color.yellow);
            if (current == end) break;

            yield return new WaitForSeconds(0.03f);

            foreach (var item in _graph.GetNeighbors(current))
            {
                if (item.layer == 6) continue; //IsBlocked ?
                //Modificar a futuro para que permita diferentes maneras de chequear por un nodo bloqueado

                if (!cameFrom.ContainsKey(item))
                {
                    frontier.Enqueue(item);
                    cameFrom.Add(item, current);
                    ChangeObjectColor(item, Color.red);
                }
            }
            ChangeObjectColor(current, Color.gray + new Color(0.1f, 0.1f, 0.1f) * 1.5f);
        }

        if (current == end)
        {
            List<GameObject> path = new List<GameObject>();
            while (current != null)
            {
                path.Add(current);
                current = cameFrom[current];
            }
            path.Reverse();
            for (int i = 0; i < path.Count; i++)
            {
                ChangeObjectColor(path[i], Color.Lerp(Color.yellow, Color.red, (float)i / path.Count));
                yield return new WaitForSeconds(0.05f);
            }

        }
    }*/

    //Semana que viene
    /*public IEnumerator PaintDijkstra(Node start, Node end)
    {
        //Queue<Node> frontier = new Queue<Node>();
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
                //path.append(start) # optional
                path.Reverse(); // optional
                return path;
            }

            foreach (var item in current.Neighbors)
            {
                if (item.isBlocked) continue;

                int newCost = costSoFar[current] + item.Cost;

                if (!costSoFar.ContainsKey(item) || newCost < costSoFar[current])
                {
                    frontier.Enqueue(item, newCost);
                    cameFrom[item] = current;
                    costSoFar[item] = newCost;
                }
            }

        }
        return default;
    }*/

    public IEnumerator PaintAStar(Node2 start, Node2 end)
    {
        var time = new WaitForSeconds(0.03f);

        var frontier = new PriorityQueue<Node2>();
        frontier.Enqueue(start, 0);
        var cameFrom = new Dictionary<Node2, Node2>();
        cameFrom.Add(start, null);

        var costSoFar = new Dictionary<Node2, int>();
        costSoFar.Add(start, 0);
        Node2 current = default;
        while (frontier.Count > 0)
        {
            current = frontier.Dequeue();

            current.ChangeColor(Color.yellow);
            yield return time;

            if (current == end) break;


            foreach (var item in current.Neighbors)
            {
                if (item.isBlocked) continue;

                int newCost = costSoFar[current] + item.Cost;

                if (!costSoFar.ContainsKey(item) || newCost < costSoFar[current])
                {
                    frontier.Enqueue(item, newCost + Heuristic(end.transform.position, item.transform.position));
                    cameFrom[item] = current;
                    costSoFar[item] = newCost;
                    item.ChangeColor(Color.blue);
                }
            }

            yield return time;
            current.ChangeColor(Color.gray);

        }

        if (current == end)
        {
            while (current != null)
            {
                current.ChangeColor(Color.green);
                yield return time;
                current = cameFrom[current];
            }
        }
    }

    #region EjemploCallback
     public IEnumerator PaintAStar(Node2 start, Node2 end, Action<List<Vector3>> callBack)
    {
        var time = new WaitForSeconds(0.03f);

        var frontier = new PriorityQueue<Node2>();
        frontier.Enqueue(start, 0);
        var cameFrom = new Dictionary<Node2, Node2>();
        cameFrom.Add(start, null);

        var costSoFar = new Dictionary<Node2, int>();
        costSoFar.Add(start, 0);
        Node2 current = default;
        while (frontier.Count > 0)
        {
            current = frontier.Dequeue();

            current.ChangeColor(Color.yellow);
            yield return time;

            if (current == end) break;


            foreach (var item in current.Neighbors)
            {
                if (item.isBlocked) continue;

                int newCost = costSoFar[current] + item.Cost;

                if (!costSoFar.ContainsKey(item) || newCost < costSoFar[current])
                {
                    frontier.Enqueue(item, newCost + Heuristic(end.transform.position, item.transform.position));
                    cameFrom[item] = current;
                    costSoFar[item] = newCost;
                    item.ChangeColor(Color.blue);
                }
            }

            yield return time;
            current.ChangeColor(Color.gray);

        }

        if (current == end)
        {
            var path = new List<Vector3>();
            while (current != start)
            {
                path.Add(current.transform.position);
                current.ChangeColor(Color.green);
                yield return time;
                current = cameFrom[current];
            }
            start.ChangeColor(Color.green);
            yield return time;
            path.Reverse();
            callBack(path);
        }
    }
    #endregion

    void ChangeObjectColor(GameObject go, Color color)
    {
        if (go == null) return;
        go.GetComponent<Renderer>().material.color = color;
    }
}
