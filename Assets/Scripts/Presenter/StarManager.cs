/*  author      : brian tria
 *  date        : april 25, 2020
 *  description : 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [SerializeField]
    private GameObject starPrefab;


    // Start is called before the first frame update
    void Start()
    {
        if (starPrefab == null)
        {
            Debug.LogError("Missing star prefab.");
            return;
        }

        for (int idx = 0; idx < 100; idx++)
        {
            GameObject starObject = Instantiate(starPrefab);
            starObject.SetActive(false);

            Transform starTransform = starObject.transform;
            starTransform.SetParent(transform);
            starTransform.localScale = Vector3.one;
            starTransform.localPosition = Vector3.zero;
        }
    }
}
