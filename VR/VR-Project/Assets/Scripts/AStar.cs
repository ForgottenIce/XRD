using System;
using System.Collections.Generic;
using System.Linq;

public class AStar
{
    public class Node : IComparable<Node>{
        public Node parent;
        public int x, y, distance;
        double weight;

        public Node(int x, int y, int distance, int[] goal, Node parent) {
            this.x = x;
            this.y = y;
            this.distance = distance;
            this.weight = distance + Math.Abs(x - (goal[0])) + Math.Abs(y - (goal[1]));
            this.parent = parent;
        }

        public int CompareTo(Node o) {
            if (weight > o.weight) return 1;
            else if (weight < o.weight) return -1;
            return 0;
        }
    }

    static private readonly int[] row = { -1, 0, 0, 1 };
    static private readonly int[] col = { 0, -1, 1, 0 };
    static public Node ShortestPath(int[][] maze, int[] start, int[] goal) //should now be using A*
    {
        // inspiration found on: https://www.techiedelight.com/lee-algorithm-shortest-path-in-a-maze/
        int height = maze.Length;
        int width = maze[0].Length;
        bool[][] visited = new bool[height][];
        for (int i  = 0; i < height; i++) {
            visited[i] = new bool[width];
        }
        List<Node> queue = new();

        visited[start[0]][start[1]] = true;
        queue.Add(new Node(start[0], start[1], 0, new int[] { start[0], start[1] }, null));

        while (queue.Count != 0) {
            Node node = queue.Min();
            queue.Remove(node);

            int i = node.x;
            int j = node.y;
            int distance = node.distance;

            if (i == goal[0] && j == goal[1]) {
                return node;
            }

            for (int k = 0; k < 4; k++) {
                int testRow = i + row[k];
                int testCol = j + col[k];
                if ((maze[testRow][testCol] != -1) && !visited[testRow][testCol]) { // TODO there are array out of bounds here why?
                    visited[testRow][testCol] = true;
                    queue.Add(new Node(testRow, testCol, distance + (int)Math.Pow(maze[testRow][testCol]+1,10), goal, node));
                }
            }
            queue.Sort();
        }
        return new Node(start[0], start[1], Int32.MaxValue, goal, null);
    }
}
