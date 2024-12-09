using Blockus_Client.GameBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockus_Client.Blocks
{
    public class Block12 : BlockTemplate
    {
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[]
            {
                new Position(1, 1),
                new Position(1, 2),
                new Position(1, 3),
                new Position(1, 4),
                new Position(1, 5),
                new Position(2, 1)
            }
        };

        private readonly Position[][] validTiles = new Position[][]
        {
            new Position[]
            {
                new Position(0, 0),
                new Position(0, 6),
                new Position(2, 6),
                new Position(3, 0),
                new Position(3, 2)
            }
        };

        private readonly Position[][] invalidTiles = new Position[][]
        {
            new Position[]
            {
                new Position(0, 1),
                new Position(0, 2),
                new Position(0, 3),
                new Position(0, 4),
                new Position(0, 5),
                new Position(1, 6),
                new Position(1, 0),
                new Position(2, 0),
                new Position(3, 1),
                new Position(2, 2),
                new Position(2, 3),
                new Position(2, 4),
                new Position(2, 5),
            }
        };

        public override int Color { get; set; } = 0;

        public override int Punctuation { get; set; } = 5;

        protected override Position[][] Tiles => tiles;

        protected override Position[][] ValidTiles => validTiles;

        protected override Position[][] InvalidTiles => invalidTiles;

        protected override Position StartOffset => new Position(10, 10);
    }
}
