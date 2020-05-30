/* author: Brian Tria
 * created: May 08, 2020
 * description: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPrediction : MonoBehaviour
{
    #region Properties

    [SerializeField]
    private Player player;

    [SerializeField]
    private AsteroidManager obstacleManager;

    private int nextSafePointIndex = 1;
    private bool isPlaying = false;

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
        if (player == null)
        {
            Debug.LogError("Missing player reference.");
        }

        if (obstacleManager == null)
        {
            Debug.LogError("Missing obstacle parent transform.");
        }

    }

    void Update()
    {
        if (!isPlaying)
        {
            return;
        }

        if (player == null)
        {
            return;
        }

        if (obstacleManager == null)
        {
            return;
        }

        Transform obstacleManagerTransform = obstacleManager.transform;
        if (obstacleManagerTransform.childCount < 2)
        {
            return;
        }

        int prevIndex = nextSafePointIndex - 1;
        if (prevIndex < 0)
        {
            prevIndex = obstacleManagerTransform.childCount - 1;
        }

        Vector3 playerPosition = player.transform.position;
        Vector3 prevPosition = obstacleManagerTransform.GetChild(prevIndex).transform.position;
        Vector3 nextSafePoint = obstacleManagerTransform.GetChild(nextSafePointIndex).transform.position;
        nextSafePoint.x = prevPosition.x;

        if (nextSafePoint.x - playerPosition.x <= -1)
        {
            nextSafePointIndex += 1;
            nextSafePointIndex = nextSafePointIndex % obstacleManagerTransform.childCount;
            nextSafePoint = obstacleManagerTransform.GetChild(nextSafePointIndex).transform.position;
        }

        if (nextSafePoint.x - playerPosition.x > 0)
        {
            player.PointToNextSafePoint(nextSafePoint);
        }
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
    }

    #endregion
}
