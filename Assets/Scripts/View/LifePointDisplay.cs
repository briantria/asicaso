/*  author      : brian tria
 *  date        : june 1, 2020
 *  description : 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePointDisplay : MonoBehaviour
{
    #region Properties

    [SerializeField]
    private GameObject lifePointPrefab;
    private List<GameObject> lifePointPool = new List<GameObject>();

    #endregion

    #region Life Cycle

    void OnEnable()
    {
        Debug.Log("set onReset life");
        LifePointSystem.OnReset += ResetLifePoints;
    }

    void OnDisable()
    {
        Debug.Log("remove onReset life");
        LifePointSystem.OnReset -= ResetLifePoints;
    }

    #endregion

    #region Private

    void ResetLifePoints(int lifePoints)
    {
        Debug.Log("onReset life");
        if (lifePointPrefab == null)
        {
            Debug.LogError("Missing life point prefab reference.");
            return;
        }

        int idx = 0;
        for (; idx < lifePoints; ++idx)
        {
            if (idx < lifePointPool.Count)
            {
                Debug.Log("activate life point");
                lifePointPool[idx].SetActive(true);
                continue;
            }

            GameObject lifePointObj = Instantiate(lifePointPrefab);
            lifePointObj.name = "[" + idx + "]";
            lifePointObj.SetActive(true);
            lifePointPool.Add(lifePointObj);

            RectTransform rTransform = lifePointObj.GetComponent<RectTransform>();
            lifePointObj.transform.SetParent(this.transform);
            Debug.Log("set parent");
            // todo: POSITION
        }

        for (; idx < lifePointPool.Count; ++idx)
        {
            lifePointPool[idx].SetActive(false);
        }
    }

    #endregion
}