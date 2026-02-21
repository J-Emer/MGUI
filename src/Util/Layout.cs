using System;
using System.Collections.Generic;
using MGUI.Controls;
using Microsoft.Xna.Framework;

namespace MGUI.Util
{
    public abstract class Layout
    {
        public abstract void HandleLayout(Rectangle bounds, List<Control> controls, int padding);
    }
    public class RowLayout : Layout
    {
        public override void HandleLayout(Rectangle bounds, List<Control> controls, int padding)
        {
            int xpos = bounds.X + padding;
            int ypos = bounds.Y + padding;
            int width = bounds.Width - (padding + padding); //...left & right sides

            foreach (var item in controls)
            {
                item.Position = new Point(xpos, ypos);
                item.Size = new Point(width, item.Size.Y);

                ypos += item.Size.Y + padding;
            }
        }
    }
    public class ColumnLayout : Layout
    {
        public override void HandleLayout(Rectangle bounds, List<Control> controls, int padding)
        {
            int xpos = bounds.X + padding;
            int ypos = bounds.Y + padding;
            int height = bounds.Height - (padding * 2);

            foreach (var item in controls)
            {
                item.Position = new Point(xpos, ypos);
                item.Size = new Point(item.Size.X, height); // stretch vertically
                xpos += item.Size.X + padding;
            }
        }
    }
    public class GridLayout : Layout
    {
        public int Columns { get; set; } = 2;
    
        public override void HandleLayout(Rectangle bounds, List<Control> controls, int padding)
        {
            int colWidth = (bounds.Width - padding * (Columns + 1)) / Columns;
            int rowHeight = 0;
            int xpos = bounds.X + padding;
            int ypos = bounds.Y + padding;
            int col = 0;
    
            foreach (var item in controls)
            {
                item.Position = new Point(xpos, ypos);
                item.Size = new Point(colWidth, item.Size.Y);
    
                rowHeight = Math.Max(rowHeight, item.Size.Y);
    
                col++;
                if (col >= Columns)
                {
                    col = 0;
                    xpos = bounds.X + padding;
                    ypos += rowHeight + padding;
                    rowHeight = 0;
                }
                else
                {
                    xpos += colWidth + padding;
                }
            }
        }
    }
    public enum StackDirection { Vertical, Horizontal }
    public class StackLayout : Layout
    {
        public StackDirection Direction { get; set; } = StackDirection.Vertical;

        public StackLayout(StackDirection direction)
        {
            this.Direction = direction;
        }
        public override void HandleLayout(Rectangle bounds, List<Control> controls, int padding)
        {
            int xpos = bounds.X + padding;
            int ypos = bounds.Y + padding;

            foreach (var item in controls)
            {
                item.Position = new Point(xpos, ypos);

                if (Direction == StackDirection.Vertical)
                {
                    ypos += item.Size.Y + padding;
                }
                else
                {
                    xpos += item.Size.X + padding;
                }
            }
        }
    }
    public class StretchLayout : Layout
    {
        public override void HandleLayout(Rectangle bounds, List<Control> controls, int padding)
        {
            Control _control = controls[0];

            int x = bounds.X + padding;
            int y = bounds.Y + padding;
            int width = bounds.Width - (padding * 2);
            int height = bounds.Height - (padding * 2);

            _control.Position = new Point(x, y);
            _control.Size = new Point(width, height);
        }
    }


}