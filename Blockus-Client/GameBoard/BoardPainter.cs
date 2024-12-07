using Blockus_Client.Blocks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Blockus_Client.GameBoard
{
    public class BoardPainter
    {

        private readonly Image[,] _imageControls;
        private readonly Image[,] _blockControls;
        private readonly ImageSource[] _tileImages;

        public BoardPainter(int rows, int columns, Canvas gameCanvas, Canvas blockCanvas, ImageSource[] tileImages)
        {
            _imageControls = InitializeCanvas(rows, columns, gameCanvas);
            _blockControls = InitializeCanvas(rows, columns, blockCanvas);
            _tileImages = tileImages;
        }

        public static Image[,] InitializeCanvas(int rows, int columns, Canvas canvas)
        {
            Image[,] imageControls = new Image[rows, columns];
            int cellSize = 25;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    Image imageControl = new Image { Width = cellSize, Height = cellSize };
                    Canvas.SetTop(imageControl, r * cellSize);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    canvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }

            return imageControls;
        }

        public void DrawGrid(GameGrid grid)
        {
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    _imageControls[r, c].Source = _tileImages[id];
                }
            }
        }

        public void DrawBlock(BlockTemplate block)
        {
            ClearCanvas(_blockControls);

            foreach (var position in block.TilePositions())
            {
                var image = _blockControls[position.Row, position.Column];
                image.Source = _tileImages[block.Color];
                image.Effect = CreateShadowEffect();
            }
        }

        public void ClearCanvas(Image[,] controls)
        {
            foreach (var control in controls)
            {
                control.Source = null;
            }
        }

        private static DropShadowEffect CreateShadowEffect()
        {
            return new DropShadowEffect
            {
                Color = Colors.Black,
                Opacity = 0.7,
                ShadowDepth = 5,
                BlurRadius = 5
            };
        }
    }
}
