using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;
using IPA.Utilities;

namespace FPS_Counter
{
    class FPSCounter : MonoBehaviour
    {
        private TextMeshProUGUI _counter;
        private GameObject _percent;
        private int _targetFramerate;
        private Image _image;
        private float _updateRate;
        private bool _displayRing;

        private void Awake()
        {
            try
            {
                Logger.Log("Attempting to Initialize FPS Counter");
                Init();
            }
            catch (Exception ex)
            {
                Logger.Log("FPS Counter Done screwed up on initialization", Logger.LogLevel.Error); // -Kyle1413
                Logger.Log(ex.ToString(), Logger.LogLevel.Error);
            }
        }

        private void Init()
        {
            _targetFramerate = (int) XRDevice.refreshRate;
            _updateRate = Config.UpdateRate;
            _displayRing = Config.ShowRing;
            Logger.Log($"Target framerate = {_targetFramerate.ToString()}");

            Canvas canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            CanvasScaler cs = gameObject.AddComponent<CanvasScaler>();
            cs.scaleFactor = 10.0f;
            cs.dynamicPixelsPerUnit = 10f;
            GraphicRaycaster gr = gameObject.AddComponent<GraphicRaycaster>();
            gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
            gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);

            Image image = null;
            try
            {
                image = ReflectionUtil.GetPrivateField<Image>(
                    Resources.FindObjectsOfTypeAll<ScoreMultiplierUIController>().First(), "_multiplierProgressImage");
            }
            catch (Exception e)
            {
                Logger.Log("oops");
            }

            _counter = CustomUI.BeatSaber.BeatSaberUI.CreateText(canvas.transform as RectTransform, $"FPS \n{_targetFramerate}", Vector2.zero);
            _counter.alignment = TextAlignmentOptions.Center;
            _counter.transform.localScale *= .12f;
            _counter.fontSize = 2.5f;
            _counter.color = Color.white;
            _counter.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
            _counter.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);
            _counter.enableWordWrapping = false;
            _counter.transform.localPosition = new Vector3(-0.1f, 3.5f, 8f);

            if (_displayRing)
            {
                _percent = new GameObject();
                _image = _percent.AddComponent<Image>();
                _percent.transform.SetParent(_counter.transform);
                _percent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 2f);
                _percent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 2f);
                _percent.transform.localScale = new Vector3(5f, 5f, 5f);
                _percent.transform.localPosition = Vector3.zero;

                _image.sprite = image?.sprite;
                _image.type = Image.Type.Filled;
                _image.fillMethod = Image.FillMethod.Radial360;
                _image.fillOrigin = (int)Image.Origin360.Top;
                _image.fillClockwise = true;
            }
        }

        float numFrames;
        float lastFrameTime;
        float nextCounterUpdate = Time.time + Config.UpdateRate;
        float ringFillPercent = 1;

        private void Update()
        {
            if (Time.time > nextCounterUpdate)
            {
                var fps = Mathf.Round(numFrames / (Time.time - lastFrameTime));
                _counter.text = $"FPS\n{fps}";
                if (_displayRing) ringFillPercent = fps / (float)_targetFramerate;
                lastFrameTime = nextCounterUpdate;
                nextCounterUpdate += _updateRate;
                numFrames = 0;
            }

            if (_displayRing) _image.fillAmount = Mathf.Lerp(_image.fillAmount, ringFillPercent, 0.5f);
            numFrames++;
        }
    }
}
