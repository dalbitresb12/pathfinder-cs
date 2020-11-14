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

    public void Move(Keys key) {
      switch (key) {
        case Keys.W:
          position.Y -= velocity;
          break;
        case Keys.S:
          position.Y += velocity;
          break;
        case Keys.A:
          position.X -= velocity;
          break;
        case Keys.D:
          position.X += velocity;
          break;
      }
    }
  }
}
