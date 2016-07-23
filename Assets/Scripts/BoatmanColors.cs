using UnityEngine;

public interface IPalette {
    Color background {get;}
    Color danger {get;}
    Color geometry {get;}
    Color player {get;}
    Color UI {get;}
}

public class Stories : IPalette {
    // http://www.colourlovers.com/palette/1811244/1001_Stories
    public static Color _background = RGB.rgb(53, 92, 125) * RGB.vdark * RGB.dark;
    public static Color _danger = RGB.rgb(246, 114, 128);
    public static Color _geometry = RGB.rgb(108, 91, 123);
    public static Color _player = RGB.rgb(192, 108, 132) * RGB.slightdark;
    public static Color _UI = RGB.rgb(248, 177, 149);
    public Color background { get { return _background; } }
    public Color danger { get { return _danger; } }
    public Color geometry { get { return _geometry; } }
    public Color player { get { return _player; } }
    public Color UI { get { return _UI; } }
}

public class Thumbelina : IPalette {
    // http://www.colourlovers.com/palette/634148/Thumbelina
    public static Color _background = RGB.rgb(171, 82, 107) * RGB.vdark * RGB.dark;
    public static Color _danger = RGB.rgb(240, 226, 164) * RGB.light;
    public static Color _geometry = RGB.rgb(188, 162, 151);
    public static Color _player = RGB.rgb(197, 206, 174) * RGB.light;
    public static Color _UI = RGB.rgb(244, 235, 195);
    public Color background { get { return _background; } }
    public Color danger { get { return _danger; } }
    public Color geometry { get { return _geometry; } }
    public Color player { get { return _player; } }
    public Color UI { get { return _UI; } }
}

public class BathHouse : IPalette {
    // http://www.colourlovers.com/palette/196117/Japanese_Bath
    public static Color _background = RGB.rgb(99, 61, 46) * RGB.dark;
    public static Color _danger = RGB.rgb(247, 175, 99);
    public static Color _geometry = RGB.rgb(191, 216, 173);
    public static Color _player = RGB.rgb(156, 221, 200);
    public static Color _UI = RGB.rgb(221, 217, 171);
    public Color background { get { return _background; } }
    public Color danger { get { return _danger; } }
    public Color geometry { get { return _geometry; } }
    public Color player { get { return _player; } }
    public Color UI { get { return _UI; } }
}

public class LoversInJapan : IPalette {
    // http://www.colourlovers.com/palette/867235/LoversInJapan
    public static Color _background = RGB.rgb(198, 164, 154) * RGB.vdark;
    public static Color _danger = RGB.rgb(233, 78, 119);
    public static Color _geometry = RGB.rgb(214, 129, 137);
    public static Color _player = RGB.rgb(198, 229, 217);
    public static Color _UI = RGB.rgb(244, 234, 213);
    public Color background { get { return _background; } }
    public Color danger { get { return _danger; } }
    public Color geometry { get { return _geometry; } }
    public Color player { get { return _player; } }
    public Color UI { get { return _UI; } }
}

public static class RGB {
    public static Color light = new Color (1.25f, 1.25f, 1.25f, 1);
    public static Color slightdark = new Color (0.9f, 0.9f, 0.9f, 1);
    public static Color dark = new Color (0.75f, 0.75f, 0.75f, 1);
    public static Color vdark = new Color (0.5f, 0.5f, 0.5f, 1);
    public static Color rgb(int r, int g, int b) {
        float cr = r / 256f;
        float cg = g / 256f;
        float cb = b / 256f;
        return new Color(cr, cg, cb, 1);
    }
}