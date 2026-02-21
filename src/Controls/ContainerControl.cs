using System.Linq;
using MGUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Controls
{
    public class ContainerControl : Control
    {
        public ControlCollection Children{get; private set;} = new ControlCollection();
        public Layout Layout{get;set;} = new RowLayout();
        public int Padding{get;set;} = 5;

        
        public ContainerControl()
        {
            Children.OnControlsChanged += AfterDirty;
        }
        protected override void AfterDirty()
        {
            Layout.HandleLayout(Bounds, Children.Controls, Padding);
        }
        public override Control HitTest(Point p)
        {
            for (int i = 0; i < Children.Controls.Count; i++)
            {
                var hit = Children.Controls[i].HitTest(p);

                if(hit != null)
                {
                    return hit;
                }
            }
            return Bounds.Contains(p)? this : null;
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);

            foreach (var control in Children.Controls.OrderByDescending(c => c.ZOrder))
            {
                control.Draw(spritebatch);
            }
        }
    }
}