using MGUI.Util;

namespace MGUI.Controls
{
    public class Panel : ContainerControl
    {
        //note: Panel is essentially a ContainerControl

        public Panel() : base()
        {
            BackgroundColor = Theme.Background;
        }
    }
}