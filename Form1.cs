namespace GameOfLife {
    public partial class Form1 : Form {
        //private readonly bool Running = true;
        private const short BoardWidth = 10;
        private const short BoardHeight = 10;
        private static Size ButtonSize = new(30, 30);
        private readonly short[] Adjacent = new short[] { -1, 0, +1 };
        private LinkedList<(short y, short x)> Alive = new();
        private LinkedList<(short y, short x)> BufferedAlive = new();
        private readonly short ButtonDistanceWidth = (short) ButtonSize.Width;
        private readonly short ButtonDistanceHeight = (short) ButtonSize.Height;
        private readonly Button[,] Board = new Button[BoardHeight, BoardHeight];
        public Form1() {
            InitializeComponent();
            DrawBoard();
            /*new Thread(() => {
                do {
                    Tick();
                    Thread.Sleep(1000);
                } while (Running);
            }).Start();*/
        }

        private void DrawBoard() {
            Button Button;
            for (short i = 0; i < BoardHeight; i++) {
                for (short j = 0; j < BoardWidth; j++) {
                    Button = new() {
                        BackColor = Color.White,
                        Size = ButtonSize,
                        Location = new(j * ButtonDistanceWidth, i * ButtonDistanceHeight),
                        FlatStyle = FlatStyle.Flat,
                        Tag = (i, j)
                    };
                    Button.Click += ButtonClick;
                    Board[i, j] = Button;
                    Controls.Add(Button);
                }
            }
        }

        /// <summary>
        /// Toggles a button color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick(object? sender, EventArgs e) {
            if (sender == null)
                return;
            Button ClickedButton = (Button) sender;
            Point ClickedButtonLocation = ClickedButton.Location;
            short Y = (short) (ClickedButtonLocation.Y / ButtonDistanceHeight);
            short X = (short) (ClickedButtonLocation.X / ButtonDistanceWidth);
            if (ClickedButton.BackColor == Color.White) {
                ClickedButton.BackColor = Color.Black;
                Alive.AddLast((Y, X));
            }
            else {
                ClickedButton.BackColor = Color.White;
                KillCell(Alive, Y, X);
            }
            //MessageBox.Show(Y.ToString() + " " + X.ToString());
        }

        private static void KillCell(LinkedList<(short, short)> LocalAlive, short Y, short X) {
            LinkedListNode<(short y, short x)>? ToRemove = LocalAlive.Find((Y, X));
            if (ToRemove == null)
                return;
            LocalAlive.Remove(ToRemove);
        }

        private static (short Y, short X) GetCellCoordinates(Button Cell) {
            if (Cell.Tag == null)
                return (-1, -1);
            return ((short, short)) Cell.Tag;
        }

        /// <summary>
        /// Advances the game generation by 1
        /// </summary>
        private void Tick() {
            BufferedAlive = new(Alive);
            int NearbyCellsCount;
            List<Button> ChangeToWhite = new();
            List<Button> ChangeToBlack = new();
            foreach ((short Y, short X) in Alive.ToArray()) {
                NearbyCellsCount = GetAdjacent(Y, X);
                if (NearbyCellsCount < 2) {
                    KillCell(BufferedAlive, Y, X);
                    ChangeToWhite.Add(Board[Y, X]);
                }
                else if (NearbyCellsCount > 3) {
                    KillCell(BufferedAlive, Y, X);
                    ChangeToWhite.Add(Board[Y, X]);
                }
                //Check if a cell should spawn
                short y, x;
                foreach (Button Cell in GetCellsIterator(Y, X)) {
                    (y, x) = GetCellCoordinates(Cell);
                    if (GetAdjacent(y, x) == 3) {
                        BufferedAlive.AddLast((y, x));
                        ChangeToBlack.Add(Cell);
                    }
                }
            }
            foreach (Button ButtonToChange in ChangeToWhite)
                ButtonToChange.BackColor = Color.White;
            foreach (Button ButtonToChange in ChangeToBlack)
                ButtonToChange.BackColor = Color.Black;
            Alive = BufferedAlive;
            LabelAlive.Text = $"Alive: {Alive.Count}";
        }

        private bool GetCell(short Y, short X, out Button? Cell) {
            if (Y >= 0 && X >= 0 && Y < BoardHeight && X < BoardWidth) {
                Cell = Board[Y, X];
                return true;
            }
            Cell = null;
            return false;
        }

        /// <summary>
        /// Returns the 8 adjacent CELLS to the
        /// specified Y and X coordinates
        /// </summary>
        /// <param name="Y"></param>
        /// <param name="X"></param>
        /// <returns></returns>
        private IEnumerable<Button> GetCellsIterator(short Y, short X) {
            short FinalY, FinalX;
            foreach ((int OffsetY, int OffsetX) in GetAbsoluteCoordsIterator()) {
                FinalY = (short) (Y + OffsetY);
                FinalX = (short) (X + OffsetX);
                if (GetCell(FinalY, FinalX, out Button? Cell))
                    yield return Cell;
            }
        }
        /// <summary>
        /// Returns the 8 adjacent COORDINATES to the
        /// specified Y and X coordinates
        /// </summary>
        /// <returns></returns>
        private IEnumerable<(short, short)> GetAbsoluteCoordsIterator() {
            foreach (short OffsetY in Adjacent) {
                foreach (short OffsetX in Adjacent) {
                    if (!(OffsetY == 0 && OffsetX == 0))
                        yield return (OffsetY, OffsetX);
                }
            }
        }

        /// <summary>
        /// Returns the number of alive cells
        /// in the (3-8) adjacent cells
        /// </summary>
        /// <param name="Y"></param>
        /// <param name="X"></param>
        /// <returns>Number of alive cells</returns>
        private short GetAdjacent(short Y, short X) {
            short AliveCells = 0;
            short FinalY, FinalX;
            foreach (Button Cell in GetCellsIterator(Y, X)) {
                //LabelDebug.Text = $"Y: {OffsetY} X: {OffsetX}";
                /*FinalY = (short) (Y + OffsetY);
                FinalX = (short) (X + OffsetX);*/
                if (false) {
                    Board[FinalY, FinalX].BackColor = Color.Cyan;
                    Update();
                    Thread.Sleep(800);
                    Board[FinalY, FinalX].BackColor = Color.White;
                    Update();
                }
                if (Cell.BackColor == Color.Black)
                    AliveCells++;
            }
            return AliveCells;
        }

        private void ButtonTick_Click(object sender, EventArgs e) => Tick();
    }
}