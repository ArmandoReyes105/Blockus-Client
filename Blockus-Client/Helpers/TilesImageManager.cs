using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;

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
            new BitmapImage(new Uri("Assets/Tiles/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilesMinimal/MinimalBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilesMinimal/MinimalGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilesMinimal/MinimalRed.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilesMinimal/MinimalYellow.png", UriKind.Relative))
        };

        private static readonly ImageSource[] tilesArt = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilesArt/ArtBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilesArt/ArtGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilesArt/ArtRed.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilesArt/ArtYellow.png", UriKind.Relative))
        };

        private static readonly ImageSource[] tilesAnniversary = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilesAnniversary/Blue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilesAnniversary/Green.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilesAnniversary/Red.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilesAnniversary/Yellow.png", UriKind.Relative))
        };

        public static ImageSource[] GetTiles(int option)
        {
            ImageSource[] tilesCollection;

            switch (option)
            {
                case 1: tilesCollection = tilesNormal; break;
                //case 2: tilesCollection = tilesMinimal; break;
                //case 3: tilesCollection = tilesArt; break;
                //case 4: tilesCollection = tilesAnniversary; break;
                default: tilesCollection = tilesNormal; break;
            }

            return tilesCollection;
        }

    }
}
