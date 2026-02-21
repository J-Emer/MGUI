

using System;
using MGUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Controls
{
    public class Slider : Control
    {
        private Rectangle sliderRect = new Rectangle();
        public Color ThumbColor{get;set;} = Theme.ButtonHover;
        public int Min{get;set;} = 0;
        public int Max{get;set;} = 100;
        private bool _canSlider = false;
        private int _value;
        public int Value
        {
            get => _value;
            set
            {
                int clamped = Math.Clamp(value, Min, Max);
                if (_value != clamped)
                {
                    _value = clamped;
                    UpdateThumbFromValue();
                    OnValueChanged?.Invoke(_value);
                }
            }
        }
        private int _dragOffsetX;
        private int _thumbWidth = 30;
        public Action<float> OnValueChanged;






        public Slider(int min, int max) : base()
        {
            Min = min;
            Max = max;
            Size = new Point(150, 30);
            BorderThickness = 1;
            BackgroundColor = Color.DarkGray;
        }
        protected override void AfterDirty()
        {
            UpdateThumbFromValue();
        }
        private void UpdateThumbFromValue()
        {
            int trackWidth = Size.X - _thumbWidth;
            if (trackWidth <= 0 || Max == Min)
                return;

            float percent = (float)(Value - Min) / (Max - Min);
            int thumbX = Position.X + (int)(percent * trackWidth);

            sliderRect = new Rectangle(
                thumbX,
                Position.Y,
                _thumbWidth,
                Size.Y);
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);

            spritebatch.Draw(AssetLoader.Pixel, sliderRect, ThumbColor);
        }
        public override void OnMouseDown(MouseEvent e)
        {
            if (sliderRect.Contains(e.Position))
            {
                _canSlider = true;
                _dragOffsetX = e.Position.X - sliderRect.X;
            }
        }
        public override void OnMouseDrag(MouseEvent e)
        {
            if (!_canSlider)
                return;

            int trackStart = Position.X;
            int trackWidth = Size.X - _thumbWidth;

            if (trackWidth <= 0 || Max == Min)
                return;

            // Center thumb under mouse
            int newThumbX = e.Position.X - (_thumbWidth / 2);

            // Clamp inside track
            newThumbX = Math.Clamp(newThumbX, trackStart, trackStart + trackWidth);

            sliderRect.X = newThumbX;

            // Convert position → value
            float percent = (float)(newThumbX - trackStart) / trackWidth;
            Value = Min + (int)((Max - Min) * percent);
        }
        public override void OnMouseUp(MouseEvent e)
        {
            _canSlider = false;
        }
    }
}