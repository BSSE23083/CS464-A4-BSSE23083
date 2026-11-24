using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// Tools > Check Text Contrast: logs every text in the open scene against the
// panel/button it sits on. Anything under 4.5 shows up as a warning.
public static class ContrastCheck
{
    [MenuItem("Tools/Check Text Contrast")]
    static void Check()
    {
        foreach (var text in Object.FindObjectsByType<TMP_Text>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            Image back = FindBackground(text.transform);
            if (back == null) continue;

            float ratio = ColourUtil.Contrast(text.color, BackColour(back));
            string msg = $"{text.name} on {back.name}: {ratio:0.00} : 1";
            if (ratio < 4.5f) Debug.LogWarning(msg, text);
            else Debug.Log(msg, text);
        }
    }

    static Image FindBackground(Transform t)
    {
        for (Transform p = t.parent; p != null; p = p.parent)
        {
            var img = p.GetComponent<Image>();
            if (img != null && img.color.a > 0.9f && img.type != Image.Type.Filled) return img;
        }
        return null;
    }

    // buttons are white images tinted by their colour block
    static Color BackColour(Image img)
    {
        var sel = img.GetComponent<Selectable>();
        if (sel != null) return img.color * sel.colors.normalColor;
        return img.color;
    }
}
