using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Util
{
    public static class AssetLoader
    {
        public static Texture2D Pixel{get; private set;}
        public static SpriteFont DefaultFont{get; private set;}

        public static void Init(GraphicsDevice _graphics, SpriteFont _defaultFont)
        {
            Pixel = new Texture2D(_graphics, 1, 1);
            Pixel.SetData(new[] { Color.White });

            DefaultFont = _defaultFont;
        }
    }
}