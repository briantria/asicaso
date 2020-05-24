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

    private Vector3 nextSafePoint;

    #endregion

    #region Life Cycle

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    void Start()
    {
        nextSafePoint = transform.position;
    }

    void Update()
    {

    }

    #endregion

    #region Public

    public void PointToNextSafePoint(Vector3 nextSafePoint)
    {
        if (this.nextSafePoint.y == nextSafePoint.y)
        {
            return;
        }

        this.nextSafePoint = nextSafePoint;
        Vector3 playerPosition = this.transform.position;
        playerPosition.z = 0;
        nextSafePoint.z = 0;

        Vector3 dir = (nextSafePoint - playerPosition).normalized;
        //Debug.Log("dir: " + dir);
        Quaternion rotation = Quaternion.FromToRotation(Vector3.right, dir);
        transform.rotation = rotation;
    }

    #endregion
}
