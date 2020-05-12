/* author: Brian Tria
 * created: May 08, 2019
 * description: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // [SerializeField]
    // private bool rotateBase = false;

    // [SerializeField]
    // private bool rotateAsteroid = false;

    // [SerializeField]
    // private bool rotateShadow = false;


    [SerializeField]
    private Transform asteroidTransform;

    [SerializeField]
    private Transform shadowTransform;

    private float deltaRotateZ = 0.0f;

    // [SerializeField]
    private float rotationSpeed;// = 30.0f;

    void Start()
    {
        rotationSpeed = Random.Range(20.0f, 26.0f);
        float randomRotationZ = Random.Range(0, 359);
        this.asteroidTransform.rotation = Quaternion.Euler(0, 0, randomRotationZ);
        this.shadowTransform.rotation = Quaternion.Euler(0, 0, randomRotationZ);
    }

    void Update()
    {
        deltaRotateZ += Time.deltaTime * rotationSpeed;
        this.asteroidTransform.rotation = Quaternion.Euler(0, 0, deltaRotateZ);
        this.shadowTransform.rotation = Quaternion.Euler(0, 0, deltaRotateZ * 0.5f);

        // if (rotateBase || rotateAsteroid || rotateShadow)
        // {
        //     deltaRotateZ += Time.deltaTime * rotationSpeed;
        // }

        // if (rotateBase)
        // {
        //     this.transform.rotation = Quaternion.Euler(0, 0, deltaRotateZ);
        // }

        // if (rotateAsteroid)
        // {
        //     this.asteroidTransform.rotation = Quaternion.Euler(0, 0, deltaRotateZ);
        // }

        // if (rotateShadow)
        // {
        //     this.shadowTransform.rotation = Quaternion.Euler(0, 0, deltaRotateZ);
        // }
    }
}
