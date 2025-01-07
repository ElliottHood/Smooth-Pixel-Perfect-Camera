using UnityEngine;

namespace CustomCamera.TwoDimensional.PixelPerfectCamera
{
    public class SmoothPixelPerfectCamera : MonoBehaviour
    {
        [SerializeField] private RenderTexture pixelatedTexture;
        [SerializeField] private RectTransform pixelatedTextureTransform;
        [SerializeField] private int pixelsPerUnit = 16;
        [Tooltip("The size of the render texture in the world in pixels")]
        [SerializeField] private Vector2Int resolution = new Vector2Int(320, 180);
        [Tooltip("The number of pixels to render outside each edge. This is to keep the world rendered when the camera is moved with things like Cinemachine Screenshake")]
        [SerializeField] private int renderPadding = 10;
        [SerializeField] private Camera worldToPixelCamera;
        [SerializeField] private Camera pixelToScreenCamera;
        Vector2Int resolutionIncludingRenderPadding => new Vector2Int(resolution.x + renderPadding * 2, resolution.y + renderPadding * 2);
        private static readonly float TEX_FIGHTING_OFFSET = 0.01f;

        #region Configuration Setup

        private void OnValidate()
        {
            UpdateRenderTextureTransform();
            UpdateRenderTextureResolution(resolutionIncludingRenderPadding);
            UpdateWorldToPixelCameraOrthoSize();
        }

        private void UpdateWorldToPixelCameraOrthoSize()
        {
            worldToPixelCamera.orthographicSize = GetOrthographicCameraSize(pixelsPerUnit, resolutionIncludingRenderPadding);
        }

        public float GetOrthographicCameraSize(int pixelsPerUnit, Vector2Int resolution)
        {
            return (float)resolution.y / pixelsPerUnit / 2;
        }

        private void UpdateRenderTextureTransform()
        {
            if (pixelatedTextureTransform == null)
                return;
            Vector2 size = new Vector2((float)resolutionIncludingRenderPadding.x / pixelsPerUnit, (float)resolutionIncludingRenderPadding.y / pixelsPerUnit);
            pixelatedTextureTransform.sizeDelta = size;
        }

        private void UpdateRenderTextureResolution(Vector2Int resolution)
        {
            if (pixelatedTexture == null)
                return;
            pixelatedTexture.Release(); // Release the texture before making changes
            pixelatedTexture.width = resolution.x;
            pixelatedTexture.height = resolution.y;
            pixelatedTexture.Create(); // Reinitialize the texture with new dimensions
        }

        #endregion

        private void LateUpdate()
        {
            ApplySubPixelMovement();
        }

        private void ApplySubPixelMovement()
        {
            Vector2 snappedPosition = SnapToNearestPixel(pixelToScreenCamera.transform.position, 1f / pixelsPerUnit);
            worldToPixelCamera.transform.position = new Vector3(snappedPosition.x + TEX_FIGHTING_OFFSET, snappedPosition.y + TEX_FIGHTING_OFFSET, worldToPixelCamera.transform.position.z);
            pixelatedTextureTransform.transform.position = snappedPosition;
        }

        public Vector3 SnapToNearestPixel(Vector3 position, float interval)
        {
            float snappedX = Mathf.Round(position.x / interval) * interval;
            float snappedY = Mathf.Round(position.y / interval) * interval;

            return new Vector3(snappedX, snappedY, position.z);
        }
    }
}
