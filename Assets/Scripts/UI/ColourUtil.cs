using UnityEngine;

// WCAG contrast, used to check the HUD text against its panel.
public static class ColourUtil
{
    static float Channel(float c)
    {
        return c <= 0.03928f ? c / 12.92f : Mathf.Pow((c + 0.055f) / 1.055f, 2.4f);
    }

    public static float Luminance(Color c)
    {
        return 0.2126f * Channel(c.r) + 0.7152f * Channel(c.g) + 0.0722f * Channel(c.b);
    }

    public static float Contrast(Color a, Color b)
    {
        float la = Luminance(a);
        float lb = Luminance(b);
        if (la < lb) { float t = la; la = lb; lb = t; }
        return (la + 0.05f) / (lb + 0.05f);
    }
}
