/* author: Brian Tria
 * created: May 08, 2020
 * description: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Properties

    [SerializeField]
    private float speed = 2.0f;

    private Vector3 nextSafePoint;
    private bool isPlaying;

    #endregion

    #region Life Cycle

    void OnEnable()
    {
        MainMenu.OnPlay += OnPlay;
        PauseMenu.OnQuit += OnQuit;
    }

    void OnDisable()
    {
        MainMenu.OnPlay -= OnPlay;
        PauseMenu.OnQuit -= OnQuit;
    }

    void Start()
    {
        nextSafePoint = transform.position;
    }

    void Update()
    {
        if (!isPlaying)
        {
            return;
        }

        float deltaPosY = Time.deltaTime * speed;
        Vector3 playerPosition = this.transform.position;
        Vector3 nextPos = playerPosition;
        nextPos.y = this.nextSafePoint.y;
        playerPosition = Vector3.MoveTowards(playerPosition, nextPos, Time.deltaTime * speed);
        this.transform.position = playerPosition;
    }

    #endregion

    #region Public

    public void PointToNextSafePoint(Vector3 nextSafePoint)
    {
        // if (this.nextSafePoint.y == nextSafePoint.y)
        // {
        //     return;
        // }

        this.nextSafePoint = nextSafePoint;
        Vector3 playerPosition = this.transform.position;
        playerPosition.z = 0;
        nextSafePoint.z = 0;

        Vector3 dir = (nextSafePoint - playerPosition).normalized;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.right, dir);
        //transform.rotation = rotation;
    }

    #endregion

    #region Private

    void OnPlay()
    {
        isPlaying = true;
    }

    void OnQuit()
    {
        isPlaying = false;
        Vector3 pos = this.transform.position;
        pos.y = 0;
        this.transform.position = pos;
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    #endregion
}
