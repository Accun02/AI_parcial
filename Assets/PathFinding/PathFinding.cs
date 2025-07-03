using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathFinding
{
    public static List<PFNodes> Astar(PFNodes start, PFNodes end, LayerMask mask)
    {
        var frontier = new PriorityQueue<PFNodes>();
        frontier.Enqueue(start, 0);
        Dictionary<PFNodes, float> costSoFar = new() { { start, 0 } };
        Dictionary<PFNodes, PFNodes> cameFrom = new() { { start, null } };

        while (!frontier.IsEmpty)
        {
            PFNodes currentInSearch = frontier.Dequeue();

            if (currentInSearch == end)
            {
                //Creación del Path
                PFNodes currentInPath = end;
                List<PFNodes> path = new();
                while (currentInPath != null) //Podemos preguntar que sea distinto de start para no agregarlo
                {
                    path.Add(currentInPath);
                    currentInPath = cameFrom[currentInPath];
                }
                path.Reverse();
                return path;
            }

            for (int i = 0; i < currentInSearch.Neighbors.Count; i++)
            {
                var next = currentInSearch.Neighbors[i];
                //if (next.Blocked || !LineOfSight(currentInSearch.transform.position, next.transform.position, mask)) continue;
                float newCost = costSoFar[currentInSearch] + next.Cost;
                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    costSoFar[next] = newCost;
                    frontier.Enqueue(next, newCost + Heuristic(end, next));
                    cameFrom[next] = currentInSearch;
                }
            }
        }
        return new();
    }

    private static float Heuristic(PFNodes a, PFNodes b)
    {
        return Mathf.Abs(a.x - b.x) +
             Mathf.Abs(a.y - b.y);
    }

    public static bool LineOfSight(Vector3 nodeA, Vector3 nodeB, LayerMask mask)
    {
        return !Physics.Linecast(nodeA, nodeB, mask);
    }
}
