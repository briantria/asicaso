﻿/* author: Brian Tria
 * created: May 08, 2020
 * description: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPrediction : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private AsteroidManager obstacleManager;

    private int nextSafePointIndex = 1;

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

        // Transform obstacleManagerTransform = obstacleManager.transform;
        // nextSafePoint = obstacleManagerTransform.GetChild(nextSafePointIndex).transform.position;
    }

    void Update()
    {
        if (player == null)
        {
            //Debug.LogError("Missing player reference.");
            return;
        }

        if (obstacleManager == null)
        {
            //Debug.LogError("Missing obstacle parent transform.");
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

        Vector3 prevPosition = obstacleManagerTransform.GetChild(prevIndex).transform.position;
        Vector3 nextSafePoint = obstacleManagerTransform.GetChild(nextSafePointIndex).transform.position;
        Vector3 playerPosition = player.transform.position;
        //playerPosition.x = prevPosition.x;
        playerPosition.y = nextSafePoint.y;
        player.transform.position = playerPosition;

        if (prevPosition.x - playerPosition.x <= 0)
        {
            Debug.Log("update next safe point");
            nextSafePointIndex += 1;
            nextSafePointIndex = nextSafePointIndex % obstacleManagerTransform.childCount;
            nextSafePoint = obstacleManagerTransform.GetChild(nextSafePointIndex).transform.position;
        }
    }
}
