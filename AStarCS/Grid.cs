using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace AStarCS {
  class Grid {
    GraphicsPath unwalkableMask;
    Point gridWorldSize;
    Node[,] grid;

    PointF nodeRadius;
    PointF nodeDiameter;
    Point gridSize;

    public Grid(GraphicsPath unwalkableMask, Point gridWorldSize, PointF nodeRadius) {
      this.unwalkableMask = unwalkableMask;
      this.gridWorldSize = gridWorldSize;
      this.nodeRadius = nodeRadius;

      nodeDiameter = new PointF(nodeRadius.X * 2, nodeRadius.Y * 2);
      gridSize = Point.Round(new PointF(gridWorldSize.X / nodeDiameter.X, gridWorldSize.Y / nodeDiameter.Y));
      CreateGrid();
    }

    public void CreateGrid() {
      grid = new Node[gridSize.X, gridSize.Y];

      for (int x = 0; x < gridSize.X; ++x) {
        for (int y = 0; y < gridSize.Y; ++y) {
          Point gridPos = new Point(x, y);
          Point worldPos = Point.Round(new PointF(x * nodeDiameter.X + nodeRadius.X, y * nodeDiameter.Y + nodeRadius.Y));
          bool walkable = !unwalkableMask.IsVisible(worldPos);
          grid[x, y] = new Node(walkable, worldPos, gridPos);
        }
      }
    }

    public List<Node> GetNeighbours(Node node) {
      List<Node> neighbours = new List<Node>();

      for (int x = -1; x <= 1; ++x) {
        for (int y = -1; y <= 1; ++y) {
          if (x == 0 && y == 0)
            continue;

          int checkX = node.gridPos.X + x;
          int checkY = node.gridPos.Y + y;

          if (checkX >= 0 && checkX < gridSize.X && checkY >= 0 && checkY < gridSize.Y) {
            neighbours.Add(grid[checkX, checkY]);
          }
        }
      }

      return neighbours;
    }

    public Node GetNodeFromWorldPoint(Point worldPos) {
      float percentX = (float)worldPos.X / gridWorldSize.X;
      float percentY = (float)worldPos.Y / gridWorldSize.Y;

      percentX = Clamp(percentX, 0f, 1f);
      percentY = Clamp(percentY, 0f, 1f);

      int x = RoundToInt((gridSize.X - 1) * percentX);
      int y = RoundToInt((gridSize.Y - 1) * percentY);
      return grid[x, y];
    }

    public void DrawGizmos(Graphics world) {
      Pen pen = new Pen(Color.Blue, 1);
      int nodeRadiusX = RoundToInt(nodeRadius.X);
      int nodeRadiusY = RoundToInt(nodeRadius.Y);

      foreach (Node node in grid) {
        Point location = new Point(node.worldPos.X - nodeRadiusX, node.worldPos.Y - nodeRadiusY);
        Size size = new Size(Point.Round(nodeDiameter));
        Rectangle rect = new Rectangle(location, size);
        world.DrawRectangle(pen, rect);
      }
    }

    public void DrawNodeOutline(Graphics world, Point worldPos) {
      Node node = GetNodeFromWorldPoint(worldPos);
      SolidBrush brush = new SolidBrush(Color.Green);
      Point nodePos = new Point(node.worldPos.X - RoundToInt(nodeRadius.X), node.worldPos.Y - RoundToInt(nodeRadius.Y));
      Size outlineSize = new Size(Point.Round(nodeDiameter));
      Rectangle rect = new Rectangle(nodePos, outlineSize);
      world.FillRectangle(brush, rect);
    }

    public static float Clamp(float value, float min, float max) {
      if (value < min)
        return min;
      else if (value > max)
        return max;
      else
        return value;
    }

    public static int RoundToInt(float value) => (int)Math.Round(value);
  }
}
