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
                case Block.Block04: return new Block04();
                case Block.Block05: return new Block05();
                case Block.Block06: return new Block06();
                case Block.Block07: return new Block07();
                case Block.Block08: return new Block08();
                case Block.Block09: return new Block09();
                case Block.Block10: return new Block10();
                case Block.Block11: return new Block11();
                case Block.Block12: return new Block12();
                case Block.Block13: return new Block13();
                case Block.Block14: return new Block14();
                case Block.Block15: return new Block15();
                case Block.Block16: return new Block16();
                case Block.Block17: return new Block17();
                case Block.Block18: return new Block18();
                case Block.Block19: return new Block19();
                case Block.Block20: return new Block20();
                case Block.Block21: return new Block21();
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
