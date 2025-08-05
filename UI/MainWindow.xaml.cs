using static ChessBot.Utils.FenService;
using static ChessBot.Logic.ChessGame;
using static ChessBot.Utils.BitboardOperations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading;


namespace ChessBot;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        CreateDynamicElements();
        PrintTheThing();
        DrawBoard("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR");
        StartGame();
        Console.WriteLine(((ulong)1 << 5) == 32);
    }

    private List<Button> Buttons = new List<Button>();
    private Button? ClickedTile = null;
    private Button? CurrentTile = null;

    private void CreateDynamicElements()
    {
        int rows = 8;
        int cols = 8;
        int count = 64;

        Chessboard.RowDefinitions.Clear();
        Chessboard.ColumnDefinitions.Clear();
        Chessboard.Children.Clear();

        for (int i = 0; i < rows; i++)
        {
            Chessboard.RowDefinitions.Add(new RowDefinition());
        }
        for (int j = 0; j < cols; j++)
        {
            Chessboard.ColumnDefinitions.Add(new ColumnDefinition());
        }

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                count--;
                Button tile = new Button
                {
                    Width = 100,
                    Height = 100,
                    Tag = $"{count}",
                    Content = $"{count}",
                    Background = (row + col) % 2 == 0 ? Brushes.White : Brushes.CadetBlue
                };
                tile.Click += OnClick;
                Grid.SetRow(tile, row);
                Grid.SetColumn(tile, col);
                Chessboard.Children.Add(tile);
                Buttons.Add(tile);
            }
        }
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        ClickedTile = sender as Button;
        if (ClickedTile?.Content!.ToString() == "" && CurrentTile is null) return;
        if (CurrentTile is null)
        {
            CurrentTile = ClickedTile;
            CurrentTile!.BorderBrush = Brushes.Gold;
            CurrentTile!.BorderThickness = new Thickness(5);
        }
        else
        {
            if (ClickedTile == CurrentTile)
            {
                CurrentTile!.BorderThickness = new Thickness(0);
                CurrentTile = null;
                return;
            }
            var fromTile = IntToBitboard(FlipIndex(Convert.ToInt32(CurrentTile.Tag)));
            var toTile = IntToBitboard(FlipIndex(Convert.ToInt32(ClickedTile.Tag)));
            ValidateHumanMove(fromTile, toTile);
            CurrentTile!.BorderThickness = new Thickness(0);
            CurrentTile = null;
        }
    }

    private Button? IndexToButton(int index)
    {
        if (index >= 0 && index < Buttons.Count)
        {
            return Buttons[index];
        }
        return null;
    }

    private void DrawBoard(string fen)
    {
        string formattedFen = ConvertToFen64(fen);
        for (int i = 0; i < 64; i++)
        {
            Button? tile = IndexToButton(i);
            if (tile != null) tile.Content = "";
            Image image = new Image();
            string imagePath = GetImagePathFromFen(i, formattedFen);
            if (imagePath != "1" && tile != null)
            {
                image.Source = new BitmapImage(new Uri(imagePath));
                image.Stretch = Stretch.Fill;
                tile.Content = image;
            }
        }
    }

    public void ReDrawBoard()
    {
        string fen = GenerateFen64();
        DrawBoard(fen);
    }
}