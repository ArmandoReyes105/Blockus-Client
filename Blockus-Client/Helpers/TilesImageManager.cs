using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;
using Blockus_Client.Properties;

namespace Blockus_Client.Helpers
{
    public class TilesImageManager
    {

        private static readonly ImageSource[] tilesNormal = new ImageSource[]
        {
            new BitmapImage(new Uri("/Resources/Tiles/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Resources/Tiles/TilesNormal/NormalBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Resources/Tiles/TilesNormal/NormalGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Resources/Tiles/TilesNormal/NormalRed.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Resources/Tiles/TilesNormal/NormalYellow.png", UriKind.Relative))
        };

        private static readonly ImageSource[] tilesMinimal = new ImageSource[]
        {
            new BitmapImage(new Uri("/Resources/Tiles/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Resources/Tiles/TilesMinimal/MinimalBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Resources/Tiles/TilesMinimal/MinimalGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Resources/Tiles/TilesMinimal/MinimalRed.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Resources/Tiles/TilesMinimal/MinimalYellow.png", UriKind.Relative))
        };

        private static readonly ImageSource[] tilesArt = new ImageSource[]
        {
            new BitmapImage(new Uri("/Resources/Tiles/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Resources/Tiles/TilesArt/ArtBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Resources/Tiles/TilesArt/ArtGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Resources/Tiles/TilesArt/ArtRed.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Resources/Tiles/TilesArt/ArtYellow.png", UriKind.Relative)),
        };

        private static readonly ImageSource[] tilesAnniversary = new ImageSource[]
        {
            new BitmapImage(new Uri("/Resources/Tiles/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Resources/Tiles/TilesAnniversary/Blue.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Resources/Tiles/TilesAnniversary/Green.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Resources/Tiles/TilesAnniversary/Red.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Resources/Tiles/TilesAnniversary/Yellow.png", UriKind.Relative))
        };

        public static ImageSource[] GetTiles(int option)
        {
            ImageSource[] tilesCollection;

            switch (option)
            {
                case 1: tilesCollection = tilesNormal; break;
                case 2: tilesCollection = tilesMinimal; break;
                case 3: tilesCollection = tilesArt; break;
                case 4: tilesCollection = tilesAnniversary; break;
                default: tilesCollection = tilesNormal; break;
            }

            return tilesCollection;
        }

    }
}
