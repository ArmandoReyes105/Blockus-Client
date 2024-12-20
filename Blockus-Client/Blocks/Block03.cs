﻿using Blockus_Client.GameBoard;

namespace Blockus_Client.Blocks
{
    public class Block03 : BlockTemplate
    {
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[]
            {
                new Position(1,2),
                new Position(2,1),
                new Position(2,2)
            }
        };

        private readonly Position[][] validTiles = new Position[][]
        {
            new Position[]
            {
                new Position(0,1),
                new Position(0,3),
                new Position(1,0),
                new Position(3,0),
                new Position(3,3)
            }
        };

        private readonly Position[][] invalidTiles = new Position[][]
        {
            new Position[]
            {
                new Position(0,2),
                new Position(1,1),
                new Position(1,3),
                new Position(2,0),
                new Position(2,3),
                new Position(3,1),
                new Position(3,2)
            }
        };

        public override int Color { get; set; } = 0;

        public override int Punctuation { get; set; } = 3; 

        protected override Position[][] Tiles => tiles;

        protected override Position[][] ValidTiles => validTiles;

        protected override Position[][] InvalidTiles => invalidTiles;

        protected override Position StartOffset => new Position(9,9);
    }
}
