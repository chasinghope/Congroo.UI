using UnityEngine;

namespace Congroo.UI
{
    /// <summary>
    /// UI°²È«Çø
    /// https://github.com/doyasu24/SafeAreaRect.git
    /// </summary>
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaRect : MonoBehaviour
    {
        private RectTransform _rectTf;

        private Rect _lastSafeArea;
        private int _lastScreenWidth;
        private int _lastScreenHeight;

        private void Awake()
        {
            _rectTf = GetComponent<RectTransform>();
            UpdateRect();
        }

        private void Update()
        {
            UpdateRect();
        }

        private void UpdateRect()
        {
            Rect safeArea = Screen.safeArea;
            int screenWidth = Screen.width;
            int screenHeight = Screen.height;

            // is same values
            if (safeArea.Equals(_lastSafeArea) && _lastScreenWidth == screenWidth && _lastScreenHeight == screenHeight)
            {
                return;
            }

            ApplySafeArea(safeArea, screenWidth, screenHeight);

            _lastSafeArea = safeArea;
            _lastScreenWidth = screenWidth;
            _lastScreenHeight = screenHeight;
        }

        private void ApplySafeArea(Rect safeArea, int screenWidth, int screenHeight)
        {
            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;
            anchorMin.x /= screenWidth;
            anchorMin.y /= screenHeight;
            anchorMax.x /= screenWidth;
            anchorMax.y /= screenHeight;

            _rectTf.anchoredPosition = Vector2.zero;
            _rectTf.sizeDelta = Vector2.zero;
            _rectTf.anchorMin = anchorMin.IsFinite() ? anchorMin : Vector2.zero;
            _rectTf.anchorMax = anchorMax.IsFinite() ? anchorMax : Vector2.one;
        }
    }
    
    
    
    public static class Vector2Extensions
    {
        public static bool IsFinite(this Vector2 v)
        {
            return v.x.IsFinite() && v.y.IsFinite();
        }

        private static bool IsFinite(this float f)
        {
            return !float.IsNaN(f) && !float.IsInfinity(f);
        }
    }
}