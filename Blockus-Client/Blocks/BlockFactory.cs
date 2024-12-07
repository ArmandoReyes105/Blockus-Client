using Blockus_Client.BlockusService;
using System;

namespace Blockus_Client.Blocks
{
    public static class BlockFactory
    {
        public static BlockTemplate CreateBlock(Block blockType)
        {
            switch (blockType) 
            {
                case Block.Block01: return new Block01();
                case Block.Block02: return new Block02();
                case Block.Block03: return new Block03();
                default: throw new ArgumentException("Bloque desconocido");
            }
        }

        public static int MapColorToTileIndex(Color color)
        {
            switch (color)
            {
                case Color.Blue: return 1;
                case Color.Green: return 2;
                case Color.Red: return 3;
                case Color.Yellow: return 4;
                default: return 0; 
            }
        }
    }
}
