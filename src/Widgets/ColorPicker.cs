using System;
using MGUI.Controls;
using Microsoft.Xna.Framework;

namespace MGUI.Widgets
{
    public class ColorPicker : Window
    {
        public Color Color{get; private set;} = new Color(0, 0, 0);
        private int r = 0;
        private int g = 0;
        private int b = 0;
        public Action<Color> OnColorChange;
        private ColorControl _colorControl;
        private Label _label;
        private Slider _rSlider;
        private Slider _gSlider;
        private Slider _bSlider;


        public ColorPicker() : base("Color Picker")
        {
            _colorControl = new ColorControl();
            _colorControl.BackgroundColor = Color.Black;
            Children.Add(_colorControl);

            _rSlider = new Slider(0, 255);
            _rSlider.ThumbColor = Color.Red;
            _rSlider.OnValueChanged += RedChanged;
            Children.Add(_rSlider);

            _gSlider = new Slider(0, 255);
            _gSlider.ThumbColor = Color.Green;
            _gSlider.OnValueChanged += GreenChanged;
            Children.Add(_gSlider);

            _bSlider = new Slider(0, 255);
            _bSlider.ThumbColor = Color.Blue;
            _bSlider.OnValueChanged += BlueChanged;
            Children.Add(_bSlider);

            _label = new Label();
            _label.Text = "Color: ";
            Children.Add(_label);

            MinSize = new Point(300, 300);
            Size = new Point(300, 300);
            Position = new Point(300, 300);
        }
        private void RedChanged(float obj)
        {
            r = (int)obj;
            HandleColor();
        }
        private void GreenChanged(float obj)
        {
            g = (int)obj;
            HandleColor();
        }
        private void BlueChanged(float obj)
        {
            b = (int)obj;
            HandleColor();
        }
        private void HandleColor()
        {
            Color = new Color(r,g,b);
            _colorControl.BackgroundColor = Color;
            _label.Text = $"Color: {r}, {g}, {b}";
            OnColorChange?.Invoke(Color);
        }
    
    
        public void SetColor(Color _color)
        {
            r = _color.R;
            g = _color.G;
            b = _color.B;

            _rSlider.SetValue(r);
            _gSlider.SetValue(g);
            _bSlider.SetValue(b);

            Color = _color;
            _colorControl.BackgroundColor = Color;
            _label.Text = $"Color: {r}, {g}, {b}";
        }
    
    }



    internal class ColorControl : Control
    {
        public ColorControl() : base()
        {
            Size = new Point(100, 100);
        }
    }
}