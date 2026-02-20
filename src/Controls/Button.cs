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
        public Color FontColor{get;set;} = Color.White;
        private Vector2 _textPos = Vector2.Zero;
        public Color HoverColor{get;set;} = Theme.ButtonHover;
        public Color NormalColor
        {
            get => _normalColor;
            set
            {
                _normalColor = value;
                BackgroundColor = _normalColor;
            }
        }
        private Color _normalColor = Color.Black;
        public Action<Button, MouseEvent> OnClick;

        public Button() : base()
        {
            BackgroundColor = _normalColor;
            Size = new Point(150, 30);
        }

        protected override void AfterDirty()
        {
            Vector2 _halfText = Font.MeasureString(Text) * 0.5f;
            _textPos = Bounds.Center.ToVector2() - _halfText;
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);

            spritebatch.DrawString(Font, Text, _textPos, FontColor);
        }
        public override void OnMouseEnter(MouseEvent e)
        {
            BackgroundColor = HoverColor;
        }        
        public override void OnMouseExit(MouseEvent e)
        {
            BackgroundColor = NormalColor;
        }
        public override void OnMouseDown(MouseEvent e)
        {
            OnClick?.Invoke(this, e);
        }
    }
}