using Blockus_Client.Blocks;

namespace Blockus_Client.GameBoard
{
    public class GameState
    {
        private BlockTemplate _currentBlock;

        public BlockTemplate CurrentBlock
        {
            get => _currentBlock;
            set
            {
                _currentBlock = value;

                if (_currentBlock != null)
                {
                    _currentBlock.Reset();

                    if (!BlockFits())
                    {
                        CurrentBlock.Move(-1, 0);
                    }
                }
                
            }
        }

        public GameGrid GameGrid { get; }

        public GameState()
        {
            GameGrid = new GameGrid(22, 22);
            CurrentBlock = null;
        }

        private bool BlockFits()
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                if (!GameGrid.IsInside(p.Row, p.Column))
                {
                    return false;
                }
            }

            return true;
        }

        public bool CanBePlaced()
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                if (!GameGrid.IsEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }

            return true;
        }

        public void PlaceBlock()
        {

            if (!CanBePlaced()) return;

            foreach (Position p in CurrentBlock.TilePositions())
            {
                GameGrid[p.Row, p.Column] = CurrentBlock.Color;
            }
        }

        public bool IsValidPosition()
        {
            bool isValid = false;

            foreach (Position p in CurrentBlock.ValidTilesPosition())
            {
                if (GameGrid[p.Row, p.Column] == CurrentBlock.Color)
                {
                    isValid = true;
                    break;
                }
            }

            return isValid;
        }

        public bool IsNextSameColor()
        {
            bool isNext = false;

            foreach (Position p in CurrentBlock.InvalidTilesPosition())
            {
                if (GameGrid[p.Row, p.Column] == CurrentBlock.Color)
                {
                    isNext = true;
                }
            }

            return isNext;
        }

        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }

        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }

        public void MoveBlockUp()
        {
            CurrentBlock.Move(-1, 0);

            if (!BlockFits())
            {
                CurrentBlock.Move(1, 0);
            }
        }

        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);

            if (!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
            }
        }

        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();

            if (!BlockFits())
            {
                CurrentBlock.RotateCCW();
            }
        }

        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();

            if (!BlockFits())
            {
                CurrentBlock.RotateCW();
            }
        }
    }
}
