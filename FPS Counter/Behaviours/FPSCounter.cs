using FPS_Counter.Settings;
using FPS_Counter.Utilities;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

namespace FPS_Counter.Behaviours
{
    internal class FPSCounter : MonoBehaviour
    {
        private TMP_Text counter;
        private GameObject percent;
        private int targetFramerate;
        private Image image;

        private float numFrames;
        private float lastFrameTime;
        private float nextCounterUpdate = Time.time + Configuration.Instance.UpdateRate;
        private float ringFillPercent = 1;

        private void Awake()
        {
            try
            {
                Logger.log.Debug("Attempting to Initialize FPS Counter");
                Init();
            }
            catch (Exception ex)
            {
                Logger.log.Error("FPS Counter Done screwed up on initialization"); // -Kyle1413
                Logger.log.Error(ex);
            }
        }

        private void Init()
        {
            targetFramerate = (int)XRDevice.refreshRate;

            Logger.log.Info($"Target framerate = {targetFramerate}");

            Canvas canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            //canvas.transform.Translate(new Vector3(0, 0, -100));

            CanvasScaler canvasScaler = gameObject.AddComponent<CanvasScaler>();
            canvasScaler.scaleFactor = 10.0f;
            canvasScaler.dynamicPixelsPerUnit = 10f;

            GraphicRaycaster graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
            gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
            gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);

            Image image = null!;
            try
            {
                ScoreMultiplierUIController scoreMultiplier = Resources.FindObjectsOfTypeAll<ScoreMultiplierUIController>().First();
                image = BS_Utils.Utilities.ReflectionUtil.GetPrivateField<Image>(scoreMultiplier, "_multiplierProgressImage");
            }
            catch (Exception ex)
            {
                Logger.log.Error("oops");
                Logger.log.Error(ex);
            }

            TextHelper.CreateText(out counter, canvas, Vector2.zero);
            counter.alignment = TextAlignmentOptions.Center;
            counter.transform.localScale *= Plugin.IsCountersPlusPresent ? 1 : 0.12f;
            counter.fontSize = 2.5f;
            counter.color = Color.white;
            counter.lineSpacing = -50f;
            counter.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
            counter.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);
            counter.enableWordWrapping = false;
            counter.transform.localPosition = Plugin.IsCountersPlusPresent ? Vector3.zero : new Vector3(-0.1f, 3.5f, 8f);

            if (Configuration.Instance.ShowRing)
            {
                percent = new GameObject();
                this.image = percent.AddComponent<Image>();
                percent.transform.SetParent(counter.transform);
                percent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 2f);
                percent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 2f);
                percent.transform.localScale = new Vector3(4f, 4f, 4f);
                percent.transform.localPosition = Vector3.zero;

                if (this.image)
                {
                    this.image.sprite = image?.sprite;
                    this.image.type = Image.Type.Filled;
                    this.image.fillMethod = Image.FillMethod.Radial360;
                    this.image.fillOrigin = (int)Image.Origin360.Top;
                    this.image.fillClockwise = true;
                }
            }
        }

        private void Update()
        {
            if (Time.time > nextCounterUpdate)
            {
                float fps = Mathf.Round(numFrames / (Time.time - lastFrameTime));
                counter.text = $"FPS\n{fps}";
                ringFillPercent = fps / targetFramerate;

                if (Configuration.Instance.UseColors)
                {
                    Color color;

                    if (ringFillPercent > 0.95f)
                    {
                        color = Color.green;
                    }
                    else if (ringFillPercent > 0.7f)
                    {
                        color = Color.yellow;
                    }
                    else if (ringFillPercent > 0.5f)
                    {
                        color = new Color(1, 0.64f, 0);
                    }
                    else
                    {
                        color = Color.red;
                    }

                    image?.SetColor(color);
                    counter.color = color;
                }

                lastFrameTime = nextCounterUpdate;
                nextCounterUpdate += Configuration.Instance.UpdateRate;
                numFrames = 0;
            }

            if (Configuration.Instance.ShowRing)
            {
                if (image)
                {
                    image.fillAmount = Mathf.Lerp(image.fillAmount, ringFillPercent, 2 * Time.deltaTime);
                }
            }

            numFrames++;
        }
    }

    internal static class ExtensionMethods
    {
        public static void SetColor(this Image image, Color color)
        {
            if (image)
            {
                image.color = color;
            }
        }
    }
}
