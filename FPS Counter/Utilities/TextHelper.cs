using BeatSaberMarkupLanguage;
using TMPro;
using UnityEngine;

namespace FPS_Counter.Utilities
{
    public class TextHelper
    {
        // Joink (from Counters+ (since its method was internal (which means inaccessible (which should probably be changed))))
        public static void CreateText(out TMP_Text tmp_text, Canvas canvas, Vector3 anchoredPosition)
        {
            float scale = 10f;
            RectTransform rectTransform = canvas.transform as RectTransform;
            rectTransform.sizeDelta = new Vector2(100, 50);

            tmp_text = BeatSaberUI.CreateText(rectTransform, string.Empty, anchoredPosition * scale);
            tmp_text.alignment = TextAlignmentOptions.Center;
            tmp_text.fontSize = 4f;
            tmp_text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 2f);
            tmp_text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 2f);
            tmp_text.enableWordWrapping = false;
            tmp_text.overflowMode = TextOverflowModes.Overflow;
        }
    }
}
