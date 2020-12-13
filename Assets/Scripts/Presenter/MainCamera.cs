using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private const float zoomSpeed = 5.0f;

    private bool zoomingOut = false;
    private bool zoomingIn = false;
    private float zoomTotalDistance;
    private float zoomDistance;
    private Vector3 camZoomInPosition;
    private Vector3 camZoomOutPosition;
    private float orthographicSizeZoomIn = 3.0f;
    private float orthographicSizeZoomOut = 5.0f;

    #region Life Cycle
    void OnEnable()
    {
        MainMenu.OnPlay += ZoomOut;
        PauseMenu.OnQuit += ZoomIn;
        GameOverMenu.OnQuit += ZoomIn;
    }

    void OnDisable()
    {
        MainMenu.OnPlay -= ZoomOut;
        PauseMenu.OnQuit -= ZoomIn;
        GameOverMenu.OnQuit -= ZoomIn;
    }

    void Start()
    {
        Camera mainCamera = Camera.main;

        camZoomOutPosition = mainCamera.transform.position;
        camZoomInPosition = camZoomOutPosition;
        camZoomInPosition.x = -3.5f;
        camZoomInPosition.y = 0.5f;

        mainCamera.transform.position = camZoomInPosition;
        mainCamera.orthographicSize = 3;

        zoomTotalDistance = Vector3.Distance(camZoomInPosition, camZoomOutPosition);
    }

    void Update()
    {
        if (zoomingOut)
        {
            zoomDistance += Time.deltaTime * zoomSpeed;
            float fractionDistance = zoomDistance / zoomTotalDistance;

            Camera mainCamera = Camera.main;
            mainCamera.transform.position = Vector3.Lerp(camZoomInPosition, camZoomOutPosition, fractionDistance);
            mainCamera.orthographicSize = Mathf.Lerp(orthographicSizeZoomIn, orthographicSizeZoomOut, fractionDistance);

            if (fractionDistance >= 0.9f)
            {
                zoomingOut = false;
            }
        }

        if (zoomingIn)
        {
            zoomDistance -= Time.deltaTime * zoomSpeed;
            float fractionDistance = zoomDistance / zoomTotalDistance;

            Camera mainCamera = Camera.main;
            mainCamera.transform.position = Vector3.Lerp(camZoomInPosition, camZoomOutPosition, fractionDistance);
            mainCamera.orthographicSize = Mathf.Lerp(orthographicSizeZoomIn, orthographicSizeZoomOut, fractionDistance);

            if (fractionDistance <= 0.0f)
            {
                zoomingIn = false;
            }
        }
    }
    #endregion

    private void ZoomOut()
    {
        zoomingOut = true;
        zoomDistance = 0;
    }

    private void ZoomIn()
    {
        zoomingIn = true;
        zoomDistance = zoomTotalDistance;
    }
}
