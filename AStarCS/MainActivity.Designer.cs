namespace AStarCS {
  partial class MainActivity {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.components = new System.ComponentModel.Container();
      this.movementTimer = new System.Windows.Forms.Timer(this.components);
      this.pathfinderTimer = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // movementTimer
      // 
      this.movementTimer.Interval = 30;
      this.movementTimer.Tick += new System.EventHandler(this.movementTimer_Tick);
      // 
      // pathfinderTimer
      // 
      this.pathfinderTimer.Interval = 30;
      this.pathfinderTimer.Tick += new System.EventHandler(this.pathfinderTimer_Tick);
      // 
      // MainActivity
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1264, 681);
      this.DoubleBuffered = true;
      this.Name = "MainActivity";
      this.Text = "Pathfinding - A*";
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainActivity_Paint);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainActivity_KeyDown);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainActivity_KeyUp);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Timer movementTimer;
    private System.Windows.Forms.Timer pathfinderTimer;
  }
}

