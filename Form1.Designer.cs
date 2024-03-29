﻿namespace GameOfLife {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            ButtonTick = new Button();
            LabelAlive = new Label();
            LabelDebug = new Label();
            ButtonPlay = new Button();
            ScrollBarSpeed = new HScrollBar();
            SuspendLayout();
            // 
            // ButtonTick
            // 
            ButtonTick.Anchor =  AnchorStyles.Bottom | AnchorStyles.Right;
            ButtonTick.AutoSize = true;
            ButtonTick.Location = new Point(713, 405);
            ButtonTick.Name = "ButtonTick";
            ButtonTick.Size = new Size(75, 25);
            ButtonTick.TabIndex = 0;
            ButtonTick.Text = "Tick";
            ButtonTick.UseVisualStyleBackColor = true;
            ButtonTick.Click += ButtonTick_Click;
            // 
            // LabelAlive
            // 
            LabelAlive.Anchor =  AnchorStyles.Top | AnchorStyles.Right;
            LabelAlive.AutoSize = true;
            LabelAlive.Location = new Point(743, 9);
            LabelAlive.Name = "LabelAlive";
            LabelAlive.Size = new Size(45, 15);
            LabelAlive.TabIndex = 1;
            LabelAlive.Text = "Alive: 0";
            // 
            // LabelDebug
            // 
            LabelDebug.Anchor =  AnchorStyles.Top | AnchorStyles.Right;
            LabelDebug.AutoSize = true;
            LabelDebug.Location = new Point(743, 24);
            LabelDebug.Name = "LabelDebug";
            LabelDebug.Size = new Size(42, 15);
            LabelDebug.TabIndex = 2;
            LabelDebug.Text = "Debug";
            // 
            // ButtonPlay
            // 
            ButtonPlay.Anchor =  AnchorStyles.Bottom | AnchorStyles.Right;
            ButtonPlay.AutoSize = true;
            ButtonPlay.Location = new Point(632, 405);
            ButtonPlay.Name = "ButtonPlay";
            ButtonPlay.Size = new Size(75, 25);
            ButtonPlay.TabIndex = 3;
            ButtonPlay.Text = "Play";
            ButtonPlay.UseVisualStyleBackColor = true;
            ButtonPlay.Click += ButtonPlay_Click;
            // 
            // ScrollBarSpeed
            // 
            ScrollBarSpeed.Dock = DockStyle.Bottom;
            ScrollBarSpeed.Location = new Point(0, 433);
            ScrollBarSpeed.Name = "ScrollBarSpeed";
            ScrollBarSpeed.Size = new Size(800, 17);
            ScrollBarSpeed.TabIndex = 4;
            ScrollBarSpeed.Scroll += ScrollBarSpeed_Scroll;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ScrollBarSpeed);
            Controls.Add(ButtonPlay);
            Controls.Add(LabelDebug);
            Controls.Add(LabelAlive);
            Controls.Add(ButtonTick);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ButtonTick;
        private Label LabelAlive;
        private Label LabelDebug;
        private Button ButtonPlay;
        private HScrollBar ScrollBarSpeed;
    }
}