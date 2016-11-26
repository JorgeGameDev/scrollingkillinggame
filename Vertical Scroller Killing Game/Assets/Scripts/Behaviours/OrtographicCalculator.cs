using UnityEngine;
using System.Collections;

/// <summary>
/// Calculates the pixel scale requireed for pixel perfect art.
/// </summary>
public class OrtographicCalculator : MonoBehaviour
{

    // Variables required for the calculator.
    [Header("Camera")]
    public float tileSize = 16f;
    private Camera _mainCamera;

    // Internal.
    private float _screenWidth;
    private float _screenHeight;

    // Use this for initialization
    void Start()
    {
        // Gets the main camera.
        _mainCamera = Camera.main;

        // Gets the screen info based on the screen.
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;

        // Used to update the camera size.
        UpdateCameraSize(1);
    }

    // Function used to update the camera size at runtime.s
    void UpdateCameraSize(byte multiplier)
    {
        // Calculates the ortographic size for the camera.
        float screenSize = ((_screenWidth / _screenHeight) * 2);
        float ortoSize = (_screenWidth / ((screenSize) * tileSize * multiplier));
        _mainCamera.orthographicSize = ortoSize;
    }
}