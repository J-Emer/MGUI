using System;
using MGUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Controls
{
    public class Button : Control
    {
        public SpriteFont Font{get;set;} = AssetLoader.DefaultFont;
        public string Text{get;set;} = "Button";
        public Color FontColor{get;set;} = Theme.TextPrimary;
        public Color NormalColor{get;set;} = Theme.ButtonNormal;
        public Color HighlightColor{get;set;} = Theme.ButtonHover;
        private Vector2 _textPos = Vector2.Zero;
        public Action<Button, MouseEvent> OnClick;


        public Button() : base()
        {
            Size = new Point(100, 30);
            BackgroundColor = Theme.ButtonNormal;
        }
        protected override void AfterDirty()
        {
            Vector2 _halfText = Font.MeasureString(Text) / 2f;
            _textPos = Center.ToVector2() - _halfText;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawString(Font, Text, _textPos, FontColor);
        }

        public override void OnMouseDown(MouseEvent e)
        {
            OnClick?.Invoke(this, e);
        }
        public override void OnMouseEnter(MouseEvent e)
        {
            BackgroundColor = HighlightColor;
        }
        public override void OnMouseLeave(MouseEvent e)
        {
            BackgroundColor = NormalColor;
        }


    }
}