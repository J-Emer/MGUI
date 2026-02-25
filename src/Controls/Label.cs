using MGUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Controls
{
    public class Label : Control
    {
        public SpriteFont Font{get;set;} = AssetLoader.DefaultFont;
        public Color FontColor{get;set;} = Theme.TextPrimary;
        public string Text{get;set;} = "Label";


        public Label() : base()
        {
            BackgroundColor = Color.Transparent;
            Size = new Point(100, 20);
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.DrawString(Font, Text, Position.ToVector2(), FontColor);
        }
    }
}