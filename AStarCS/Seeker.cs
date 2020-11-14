using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AStarCS {
  class Seeker {
    public Point position;
    public Size size;
    public int velocity;
    public Color color;
    public List<Node> path;

    public Seeker(Point position, Size size, int velocity, Color color) {
      this.position = position;
      this.size = size;
      this.velocity = velocity;
      this.color = color;
    }

    public void Draw(Graphics world) {
      SolidBrush brush = new SolidBrush(color);
      Rectangle rect = new Rectangle(position, size);
      world.FillRectangle(brush, rect);
    }

    public void Move(Timer timer) {
      if (path == null) {
        return;
      }

      if (path.Count == 0) {
        timer.Stop();
        return;
      }

      if (!timer.Enabled) {
        timer.Start();
      }

      Node currentNode = path[path.Count - 1];
      Point currentWaypoint = currentNode.worldPos;
      int deltaX = currentWaypoint.X - position.X;
      int deltaY = currentWaypoint.Y - position.Y;

      if (deltaX != 0)
        position.X += deltaX < 0 ? -velocity : velocity;
      if (deltaY != 0)
        position.Y += deltaY < 0 ? -velocity : velocity;

      if (position == currentWaypoint) {
        path.Remove(currentNode);
      }
    }
  }
}
