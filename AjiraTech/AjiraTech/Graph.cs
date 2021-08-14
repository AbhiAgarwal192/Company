using System.Collections.Generic;

namespace AjiraTech
{
    public class Graph
    {
        public int NumberOfEdges(int startNode, int endNode, List<int>[] adj, int numberOfVertices)
        {
            if (startNode == endNode)
            {
                return 0;
            }

            var queue = new Queue<int>();
            int[] distance = new int[numberOfVertices];
            queue.Enqueue(startNode);
            var visited = new HashSet<int>();
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                visited.Add(node);
                foreach (var item in adj[node])
                {
                    if (visited.Contains(item))
                    {
                        continue;
                    }
                    distance[item] = distance[node] + 1;
                    if (item == endNode)
                    {
                        return distance[endNode];
                    }
                    else
                    {
                        queue.Enqueue(item);
                    }
                }
            }

            return distance[endNode];
        }
    }
}
