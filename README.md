# Smooth Pixel-Perfect Camera

Achieve a low-resolution effect while keeping consistent shapes for sprites and objects that are not aligned perfectly with a grid without rough pixel snapping.
Upscaling the low-resolution effect again allows us to get sub-pixel camera movement and smooth post processing effects

[![Video Title](./GithubResources/Thumbnail.png)](https://www.youtube.com/watch?v=fMiD_gkmqSw)

Implementation:
- The scene is rendered by the World -> Pixel camera, downscaled, and sent to a render texture
    - Both the render texture and the camera are moved to the Pixel -> Screen camera's position and snapped to the nearest pixel to keep edges consistent
- Then the Pixel -> Screen camera re-renders the render texture at full resolution with the subpixel offset
    - This is where we apply post-processing effects like bloom so that those effects run at full resolution

 The Pixel -> Screen camera is the MAIN CAMERA, and therefore should have the CinemachineBrain component
 
 For other resolutions, you can change the value in the SmoothPixelPerfect script

## References

- Inspired by aarthificial, who has created an excellent explanation on the topic: https://www.youtube.com/watch?v=jguyR4yJb1M

## Known Bugs / Quirks 

- Since the main camera can only see the Pixelated and UI layers, all Virtual Camera's MUST be on the Pixelated/UI layer or they won't affect the Cinemachine Brain
- Interactable UI elements (including GameObjects with OnMouseDown()) must be on the UI layer, 
    - Otherwise the Main Camera will not render them and therefore not be able to hit them with a raycast
- The provided 3x5 pixel-perfect font isn't always perfectly aligned due imperfections in from TextMeshPro's mesh rendering algorithm

## Dependencies

- Cinemachine

## Installation

### Using Unity Package Manager

1. Set layer 3 to be 'Pixelated'
1. Go to **Window > Package Manager**.
2. Install Cinemachine
3. Click the **+** button in the top-left corner.
4. Select **Add package from git URL...**.
5. Enter the following URL: https://github.com/ElliottHood/Effector-Values.git
