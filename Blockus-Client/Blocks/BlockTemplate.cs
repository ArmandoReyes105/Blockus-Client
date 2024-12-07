using Blockus_Client.GameBoard;
using System.Collections.Generic;

namespace Blockus_Client.Blocks
{
    public abstract class BlockTemplate
    {
        protected abstract Position[][] Tiles { get; }

        protected abstract Position[][] ValidTiles { get; }

        protected abstract Position[][] InvalidTiles { get; }

        protected abstract Position StartOffset { get; }

        public abstract int Color { get; set; }

        public abstract int Punctuation { get; set; }

        private int rotationState;
        private Position _offset;

        protected BlockTemplate()
        {
            _offset = new Position(StartOffset.Row, StartOffset.Column);
        }

        public IEnumerable<Position> TilePositions()
        {
            foreach (Position p in Tiles[rotationState])
            {
                yield return new Position(p.Row + _offset.Row, p.Column + _offset.Column);
            }
        }

        public IEnumerable<Position> ValidTilesPosition()
        {
            foreach (Position p in ValidTiles[rotationState])
            {
                yield return new Position(p.Row + _offset.Row, p.Column + _offset.Column);
            }
        }

        public IEnumerable<Position> InvalidTilesPosition()
        {
            foreach (Position p in InvalidTiles[rotationState])
            {
                yield return new Position(p.Row + _offset.Row, p.Column + _offset.Column);
            }
        }

        public void Move(int rows, int columns)
        {
            _offset.Row += rows;
            _offset.Column += columns;
        }

        public void Reset()
        {
            rotationState = 0;
            _offset.Row = StartOffset.Row;
            _offset.Column = StartOffset.Column;
        }

        public void RotateCW()
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }

        public void RotateCCW()
        {
            if (rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState--;
            }
        }
    }
}
