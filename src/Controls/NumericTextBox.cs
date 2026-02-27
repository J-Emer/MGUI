using System;
using MGUI.Util;
using Microsoft.Xna.Framework;

namespace MGUI.Controls
{
    public class NumericTextBox : TextBox
    {
        public int Value{get; private set;} = 0;
        public Action<int> OnValueChanged;


        public NumericTextBox() : base()
        {
            Text = "0";
            IsActive = false;
            BorderColor = Theme.BorderLight;
            BorderThickness = 1;
            BackgroundColor = Theme.Background;
            Size = new Point(150, 25);
        }
        protected override bool IsCharAllowed(char c)
        {
            Logger.Log(this, c);
            return char.IsDigit(c);
        }
        protected override void HandleTextFinished()
        {
            Value = int.Parse(Text);
            OnValueChanged?.Invoke(Value);
        }
    }
}