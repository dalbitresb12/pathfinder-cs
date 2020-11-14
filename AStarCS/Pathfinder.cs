using System;
using System.Drawing;
using System.Collections.Generic;

namespace AStarCS {
  class Pathfinder {
    private Pathfinder() {}

    public static void FindPath(Grid grid, Point startPos, Point targetPos, Seeker seeker) {
      Node startNode = grid.GetNodeFromWorldPoint(startPos);
      Node targetNode = grid.GetNodeFromWorldPoint(targetPos);

      List<Node> openNodes = new List<Node>();
      HashSet<Node> closedNodes = new HashSet<Node>();
      
      openNodes.Add(startNode);

      while (openNodes.Count > 0) {
        Node node = openNodes[0];
        for (int i = 1; i < openNodes.Count; ++i) {
          if (openNodes[i].fCost < node.fCost || openNodes[i].fCost == node.fCost) {
            if (openNodes[i].hCost < node.hCost)
              node = openNodes[i];
          }
        }

        openNodes.Remove(node);
        closedNodes.Add(node);

        if (node == targetNode)
          seeker.path = RetracePath(startNode, targetNode);

        foreach (Node neighbour in grid.GetNeighbours(node)) {
          if (!neighbour.walkable || closedNodes.Contains(neighbour))
            continue;

          int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
          if (newCostToNeighbour < neighbour.gCost || !openNodes.Contains(neighbour)) {
            neighbour.gCost = newCostToNeighbour;
            neighbour.hCost = GetDistance(neighbour, targetNode);
            neighbour.parent = node;

            if (!openNodes.Contains(neighbour)) {
              openNodes.Add(neighbour);
            }
          }
        }
      }
    }

    public static List<Node> RetracePath(Node startNode, Node targetNode) {
      List<Node> path = new List<Node>();
      Node currentNode = targetNode;

      while (currentNode != startNode) {
        path.Add(currentNode);
        currentNode = currentNode.parent;
      }

      return path;
    }

    public static int GetDistance(Node a, Node b) {
      int distanceX = Math.Abs(a.gridPos.X - b.gridPos.X);
      int distanceY = Math.Abs(a.gridPos.Y - b.gridPos.Y);

      if (distanceX > distanceY)
        return 14 * distanceY + 10 * (distanceX - distanceY);
      return 14 * distanceX + 10 * (distanceY - distanceX);
    }
  }
}
