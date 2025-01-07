using CustomCamera;
using CustomCamera.TwoDimensional;
using System.Collections;
using UnityEngine;

public class CameraEffectButton : MonoBehaviour
{
    [SerializeField] private EffectType effectType;
    [SerializeField] private Color pressedColor = Color.gray;
    [SerializeField] private ScreenShakeSettings screenShakeSettings = ScreenShakeSettings.Default;
    [SerializeField] private ScreenPullSettings screenPullSettings = ScreenPullSettings.Default;
    [SerializeField] private VirtualCamera2D defaultVCam;
    [SerializeField] private VirtualCamera2D zoomVCam;
    private SpriteRenderer sprite;
    private Color startColor;
    private bool zoomed = false;

    private enum EffectType 
    { 
        Shake,
        Pull,
        Zoom
    }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        startColor = sprite.color;
    }

    private void Start()
    {
        UpdateZoom();
    }

    private void OnMouseDown()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeColorCoroutine());
    }

    private IEnumerator ChangeColorCoroutine()
    {
        switch (effectType)
        {
            case EffectType.Shake:
                screenShakeSettings.Play();
                break;
            case EffectType.Pull:
                screenPullSettings.Play();
                break;
            case EffectType.Zoom:
                zoomed = !zoomed;
                UpdateZoom();
                break;
            default:
                break;
        }

        sprite.color = pressedColor;
        yield return new WaitForSeconds(0.25f);
        sprite.color = startColor;
    }

    private void UpdateZoom()
    {
        if (effectType != EffectType.Zoom)
            return;

        if (zoomed)
        {
            CameraSwitcher.SwitchCamera(zoomVCam);
        }
        else
        {
            CameraSwitcher.SwitchCamera(defaultVCam);
        }
    }
}
