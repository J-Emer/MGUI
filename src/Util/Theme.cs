using Microsoft.Xna.Framework;

namespace MGUI.Util
{
    public static class Theme
    {
        // ==========================
        // Core Backgrounds
        // ==========================

        public static readonly Color Background        = new Color(30, 30, 30);   // Main app bg
        public static readonly Color WindowBackground  = new Color(37, 37, 38);
        public static readonly Color HeaderBackground  = new Color(45, 45, 48);

        // ==========================
        // Borders
        // ==========================

        public static readonly Color Border            = new Color(60, 60, 60);
        public static readonly Color BorderLight       = new Color(80, 80, 80);

        // ==========================
        // Buttons
        // ==========================

        public static readonly Color ButtonNormal      = new Color(50, 50, 50);
        public static readonly Color ButtonHover       = new Color(36, 125, 209);
        public static readonly Color ButtonPressed     = new Color(0, 122, 204);

        // ==========================
        // Text
        // ==========================

        public static readonly Color TextPrimary       = new Color(220, 220, 220);
        public static readonly Color TextSecondary     = new Color(160, 160, 160);
        public static readonly Color TextDisabled      = new Color(110, 110, 110);

        // ==========================
        // Accent
        // ==========================

        public static readonly Color AccentBlue        = new Color(0, 122, 204);
        public static readonly Color AccentBlueSoft    = new Color(14, 99, 156);

        // ==========================
        // Selection & Focus
        // ==========================

        public static readonly Color Selection         = new Color(51, 153, 255, 90);
        public static readonly Color FocusOutline      = new Color(0, 122, 204);

        // ==========================
        // Dock Highlight
        // ==========================

        public static readonly Color DockPreview       = new Color(0, 122, 204, 100);
    }
}
