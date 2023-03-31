namespace GameOfLife {
    public partial class Form1 : Form {
        private readonly bool Running = true;
        private const short BoardWidth = 10;
        private const short BoardHeight = 10;
        private static Size ButtonSize = new(30, 30);
        private short[] Adjacent = new short[] { -1, 0, +1 };
        private readonly short ButtonDistanceWidth = (short) ButtonSize.Width;
        private readonly short ButtonDistanceHeight = (short) ButtonSize.Height;
        private LinkedList<(short y, short x)> Alive = new();
        private LinkedList<(short y, short x)> BufferedAlive = new();
        private Button[,] Board = new Button[BoardHeight, BoardHeight];
        private Button[,] BufferedBoard = new Button[BoardHeight, BoardHeight];
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
                        Location = new(i * ButtonDistanceHeight, j * ButtonDistanceWidth),
                        FlatStyle = FlatStyle.Flat
                    };
                    Button.Click += ButtonClick;
                    Board[i, j] = Button;
                    Controls.Add(Button);
                }
            }
        }

        private void ButtonClick(object? sender, EventArgs e) {
            if (sender == null)
                return;
            Button ClickedButton = (Button) sender;
            Point ClickedButtonLocation = ClickedButton.Location;
            short X = (short) (ClickedButtonLocation.Y / ButtonDistanceHeight);
            short Y = (short) (ClickedButtonLocation.X / ButtonDistanceWidth);
            if (ClickedButton.BackColor == Color.White) {
                ClickedButton.BackColor = Color.Black;
                Alive.AddLast((Y, X));
            }
            else {
                ClickedButton.BackColor = Color.White;
                KillCell(Board, Alive, Y, X);
            }
            //MessageBox.Show(Y.ToString() + " " + X.ToString());
        }

        private void KillCell(Button[,] LocalBoard, LinkedList<(short, short)> LocalAlive, short Y, short X) {
            LinkedListNode<(short y, short x)>? ToRemove = LocalAlive.Find((Y, X));
            if (ToRemove == null)
                return;
            LocalAlive.Remove(ToRemove);
            //LocalBoard[(Y, X)].BackColor = Color.White;
        }

        /// <summary>
        /// Advances the game generation by 1
        /// </summary>
        private void Tick() {
            //BufferedBoard = Board.co
            BufferedAlive = new(Alive);
            int NearbyCellsCount;
            List<Button> ToChange = new();
            foreach ((short Y, short X) in Alive.ToArray()) {
                NearbyCellsCount = GetAdjacent(Y, X);
                if (NearbyCellsCount < 2) {
                    KillCell(BufferedBoard, BufferedAlive, Y, X);
                    ToChange.Add(Board[Y, X]);
                }
                else if (NearbyCellsCount > 3) {
                    KillCell(BufferedBoard, BufferedAlive, Y, X);
                    ToChange.Add(Board[Y, X]);
                }
                //Check if a cell should spawn

            }
            foreach (Button ButtonToChange in ToChange) {
                ButtonToChange.BackColor = Color.White;
            }
            //Board = BufferedBoard;
            Alive = BufferedAlive;
            LabelAlive.Text = $"Alive: {Alive.Count}";
        }

        private IEnumerable<(int, int)> GetSquareIterator() {
            foreach (short OffsetY in Adjacent) {
                foreach (short OffsetX in Adjacent) {
                    if (OffsetY != 0 && OffsetX != 0)
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
            foreach ((int OffsetY, int OffsetX) in GetSquareIterator()) {
                FinalY = (short) (Y + OffsetY);
                FinalX = (short) (X + OffsetX);
                /*Board[FinalX, FinalY].BackColor = Color.Cyan;
                Update();
                Thread.Sleep(100);
                Board[FinalX, FinalY].BackColor = Color.White;
                Update();*/
                if (FinalY >= 0
                    && FinalX >= 0
                    && Board[FinalX, FinalY].BackColor == Color.Black)
                    AliveCells++;
            }
            return AliveCells;
        }

        private void ButtonTick_Click(object sender, EventArgs e) => Tick();
    }
}