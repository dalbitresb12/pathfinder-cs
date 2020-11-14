using System;
using System.Drawing;

namespace AStarCS {
  class Node {
    public readonly Point worldPos;
    public readonly Point gridPos;
    public bool walkable;

    public int gCost = 0, hCost = 0;
    public Node parent = null;

    public int fCost => gCost + hCost;

    public Node(bool walkable, Point worldPos, Point gridPos) {
      this.walkable = walkable;
      this.worldPos = worldPos;
      this.gridPos = gridPos;
    }
  }
}
