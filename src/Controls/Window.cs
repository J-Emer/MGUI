using MGUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Controls
{
    public class Window : ContainerControl
    {
        public int HeaderHeight{get;set;} = 30;
        public SpriteFont Font{get;set;} = AssetLoader.DefaultFont;
        public string HeaderText{get;set;}
        private Rectangle _headerRect = new Rectangle();
        private Rectangle _bodyRect = new Rectangle();
        public Color HeaderColor{get;set;} = Theme.HeaderBackground;
        public Color FontColor{get;set;} = Theme.TextPrimary;
        private Vector2 _textPos = Vector2.Zero;


        public Window(string name) : base()
        {
            Name = name;
            HeaderText = name;
            Size = new Point(300, 400);
        }

        protected override void AfterDirty()
        {
            _headerRect = new Rectangle(Position.X, Position.Y, Size.X, HeaderHeight);
            _bodyRect = new Rectangle(
                Position.X,
                Position.Y + HeaderHeight,
                Size.X,
                Size.Y - HeaderHeight
            );

            Vector2 _halfText = Font.MeasureString(HeaderText) * 0.5f;
            _textPos = Bounds.Center.ToVector2() - _halfText;

            Layout.HandleLayout(_bodyRect, Children.Controls, Padding);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);
            spritebatch.Draw(AssetLoader.Pixel, _headerRect, HeaderColor);
            spritebatch.DrawString(Font, HeaderText, _textPos, FontColor);
        }
    }
}