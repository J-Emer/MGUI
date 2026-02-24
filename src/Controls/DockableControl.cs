using MGUI.Util;

namespace MGUI.Controls
{
    public class DockableControl : Control
    {
        public DockStyle Dock
        {
            get => dock;
            set
            {
                if(dock != value)
                {
                    dock = value;
                    UIManager.Instance.ChildDockChanged();
                }
            }

        }
        protected DockStyle dock = DockStyle.None;
    }
}