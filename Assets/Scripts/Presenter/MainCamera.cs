using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private const float zoomSpeed = 5.0f;

    private bool zoomingOut = false;
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
    }

    void OnDisable()
    {
        MainMenu.OnPlay -= ZoomOut;
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

            /*
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
            */
        }
    }
    #endregion

    private void ZoomOut()
    {
        zoomingOut = true;
        zoomDistance = 0;
    }
}
