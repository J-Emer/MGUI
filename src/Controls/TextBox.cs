using System;
using MGUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGUI.Controls
{
    public class TextBox : Control
    {
        public SpriteFont Font{get;set;} = AssetLoader.DefaultFont;
        public string Text{get;set;} = "TextBox";
        public Color FontColor{get;set;} = Theme.TextPrimary;
        private int Padding = 5;
        private Point _textPos = Point.Zero;
        private bool _handleInput = false;


        public TextBox() : base()
        {
            BorderColor = Theme.BorderLight;
            BorderThickness = 1;
            BackgroundColor = Theme.Background;
            Size = new Point(150, 25);
        }
        protected override void AfterDirty()
        {
            _textPos = Position + new Point(Padding, Padding);    
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);

            spritebatch.DrawString(Font, Text, _textPos.ToVector2(), FontColor); 
        }



        public override void OnKeyDown(Keys key)
        {
            if(!IsActive){return;}
            Logger.Log(this, key);
        }
    }
}