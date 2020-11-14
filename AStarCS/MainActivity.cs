using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AStarCS {
  public partial class MainActivity : Form {
    Grid grid;
    Player player;
    Seeker seeker1;
    Seeker seeker2;
    Seeker seeker3;
    Seeker seeker4;
    List<Keys> keysPressed;
    List<Keys> validKeys;

    public MainActivity() {
      InitializeComponent();

      GraphicsPath unwalkable = new GraphicsPath();
      Point gridWorldSize = new Point(Width, Height);
      PointF nodeRadius = new PointF(18, 10);
      grid = new Grid(unwalkable, gridWorldSize, nodeRadius);

      player = new Player(new Point(100, 30), new Size(10, 10), 3);

      seeker1 = new Seeker(new Point(400, 200), new Size(10, 10), 2, Color.DarkBlue);
      seeker2 = new Seeker(new Point(300, 500), new Size(10, 10), 2, Color.Yellow);
      seeker3 = new Seeker(new Point(100, 432), new Size(10, 10), 2, Color.Black);
      seeker4 = new Seeker(new Point(800, 345), new Size(10, 10), 2, Color.DarkMagenta);

      keysPressed = new List<Keys>();
      validKeys = new List<Keys>() { Keys.W, Keys.S, Keys.A, Keys.D };
    }

    private void MainActivity_Paint(object sender, PaintEventArgs e) {
      Graphics world = e.Graphics;
      grid.DrawNodeOutline(world, player.position);
      grid.DrawGizmos(world);
      player.Draw(world);
      seeker1.Draw(world);
      seeker2.Draw(world);
      seeker3.Draw(world);
      seeker4.Draw(world);
    }

    private void MainActivity_KeyDown(object sender, KeyEventArgs e) {
      if (!validKeys.Contains(e.KeyCode))
        return;

      if (!movementTimer.Enabled)
        movementTimer.Start();

      if (!keysPressed.Contains(e.KeyCode))
        keysPressed.Add(e.KeyCode);
    }

    private void MainActivity_KeyUp(object sender, KeyEventArgs e) {
      if (keysPressed.Count < 2)
        movementTimer.Stop();

      if (keysPressed.Contains(e.KeyCode))
        keysPressed.Remove(e.KeyCode);
    }

    private void movementTimer_Tick(object sender, EventArgs e) {
      keysPressed.ForEach(key => player.Move(key));
      Pathfinder.FindPath(grid, seeker1.position, player.position, seeker1);
      Pathfinder.FindPath(grid, seeker2.position, player.position, seeker2);
      Pathfinder.FindPath(grid, seeker3.position, player.position, seeker3);
      Pathfinder.FindPath(grid, seeker4.position, player.position, seeker4);

      if (!pathfinderTimer.Enabled)
        pathfinderTimer.Start();

      Refresh();
    }

    private void pathfinderTimer_Tick(object sender, EventArgs e) {
      seeker1.Move(pathfinderTimer);
      seeker2.Move(pathfinderTimer);
      seeker3.Move(pathfinderTimer);
      seeker4.Move(pathfinderTimer);
      Refresh();
    }
  }
}
