using System;
using System.Drawing;
using System.Windows.Forms;

namespace AStarCS {
  class Player {
    public Point position;
    public Size size;
    public int velocity;

    public Player(Point position, Size size, int velocity) {
      this.position = position;
      this.size = size;
      this.velocity = velocity;
    }

    public void Draw(Graphics world) {
      SolidBrush brush = new SolidBrush(Color.Red);
      Rectangle rect = new Rectangle(position, size);
      world.FillRectangle(brush, rect);
    }

    public void Move(Keys key, Grid grid) {
      Point mainCorner = new Point(position.X, position.Y);

      switch (key) {
        case Keys.W:
          mainCorner.Y -= velocity;
          break;
        case Keys.S:
          mainCorner.Y += velocity;
          break;
        case Keys.A:
          mainCorner.X -= velocity;
          break;
        case Keys.D:
          mainCorner.X += velocity;
          break;
      }

      Point inverseCorner = new Point(mainCorner.X + size.Width, mainCorner.Y + size.Height);
      Point corner1 = new Point(mainCorner.X + size.Width, mainCorner.Y);
      Point corner2 = new Point(mainCorner.X, mainCorner.Y + size.Height);

      if (!grid.unwalkableMask.IsVisible(mainCorner) && !grid.unwalkableMask.IsVisible(inverseCorner)
        && !grid.unwalkableMask.IsVisible(corner1) && !grid.unwalkableMask.IsVisible(corner2)) {
        position = mainCorner;
      }
    }
  }
}
