/* author: Brian Tria
 * created: May 08, 2019
 * description: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enum Layer
// {
//     Background,
//     Background2,
//     Midground,
//     Foreground
// }

public class AsteroidManager : MonoBehaviour
{
    #region Properties

    [SerializeField]
    private GameObject asteroidPrefab;

    [SerializeField]
    private bool isBackground = true;

    [SerializeField]
    private Vector2 scaleRange = Vector2.one;

    [SerializeField]
    private float verticalSpan = 2.0f;

    [SerializeField]
    private float horizontalSpan = 2.0f;

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private int maxAsteroidPoolCount = 10;

    [SerializeField]
    private int clusterAsteroidCount = 1;

    private List<GameObject> asteroidPool = new List<GameObject>();
    private Vector3 vanishingPoint;
    private Vector3 spawnPoint;
    private int lastSpawnIndex;
    private Vector3 lastClusterPosition;
    private Camera mainCamera;

    #endregion

    #region LifeCycle

    void Start()
    {
        mainCamera = Camera.main;

        vanishingPoint = mainCamera.ViewportToWorldPoint(Vector3.zero);
        vanishingPoint.z = 0;
        vanishingPoint.x -= 3.0f;

        spawnPoint = mainCamera.ViewportToWorldPoint(Vector3.one);
        spawnPoint.z = 0;
        spawnPoint.x += 3.0f;

        lastClusterPosition = spawnPoint;
        lastSpawnIndex = -1;

        for (int idx = 0; idx < maxAsteroidPoolCount; ++idx)
        {
            GameObject asteroidObj = Instantiate(asteroidPrefab);
            //asteroidObj.SetActive(false);
            asteroidObj.name = "[" + idx + "]";
            asteroidPool.Add(asteroidObj);
            this.Spawn(idx);
        }
    }

    void Update()
    {

    }

    #endregion

    #region Private

    void Spawn(int index)
    {
        Vector3 lastPosition = spawnPoint;
        if (lastSpawnIndex >= 0 && lastSpawnIndex < maxAsteroidPoolCount)
        {
            Transform lastTransform = asteroidPool[lastSpawnIndex].transform;
            lastPosition = lastTransform.position;
        }

        Transform asteroidTransform = asteroidPool[index].transform;

        float posX = index * horizontalSpan;
        float posY = Random.Range(0.3f, verticalSpan);

        if (index % 2 == 0)
        {
            posY *= -1;
        }

        if (isBackground)
        {

        }

        asteroidTransform.parent = this.transform;
        asteroidTransform.localScale = Vector3.one * Random.Range(scaleRange.x, scaleRange.y);
        asteroidTransform.localPosition = new Vector3(posX, posY, 0);

        lastSpawnIndex = index;
    }

    #endregion

    // private List<GameObject> asteroidBackgroundPool2 = new List<GameObject>();
    // private List<GameObject> asteroidBackgroundPool = new List<GameObject>();

    // private int asteroidMaxCount = 8;
    // private int asteroidBGCount2 = 50;
    // private int asteroidBGCount = 100;
    // private int asteroidBGClusterCount = 5;

    // private Vector3 vanishingPoint;
    // private int lastAsteroidIndex;
    // private int lastBgAsteroidIndex;
    // private int lastBgAsteroidIndex2;

    // [SerializeField]
    // private float speed = 1.0f;
    // private float spacing = 5.0f;


    // // TODO: 2+ layers of background asteroids

    // void Start()
    // {
    //     Camera camera = Camera.main;
    //     vanishingPoint = camera.ViewportToWorldPoint(Vector3.zero);
    //     vanishingPoint.z = 0;
    //     vanishingPoint.x -= 5.0f;

    //     // background
    //     int asteroidBGMaxCluster = asteroidBGCount / asteroidBGClusterCount;
    //     for (int idx = 0; idx < asteroidBGMaxCluster; ++idx)
    //     {
    //         for (int jdx = 0; jdx < asteroidBGClusterCount; ++jdx)
    //         {
    //             GameObject asteroidObj = Instantiate(asteroidPrefab);
    //             Transform asteroidTransform = asteroidObj.transform;
    //             asteroidTransform.parent = this.transform;

    //             asteroidTransform.localScale = Vector3.one * Random.Range(0.2f, 0.3f);

    //             float randX = ((idx - 2) * 2.0f) + Random.Range(0.0f, 1.8f);
    //             float randY = Random.Range(0.0f, 1.0f);
    //             if (jdx % 2 == 0) randY *= -1.0f;
    //             asteroidTransform.localPosition = new Vector3(randX, randY, 2);

    //             //asteroidObj.SetActive(false);
    //             asteroidObj.name = "BG Asteroid [" + ((idx * asteroidBGClusterCount) + jdx) + "]";
    //             asteroidBackgroundPool.Add(asteroidObj);
    //         }
    //     }

    //     lastBgAsteroidIndex = asteroidBGCount - 1;

    //     // background 2
    //     for (int idx = 0; idx < asteroidBGCount2; ++idx)
    //     {
    //         GameObject asteroidObj = Instantiate(asteroidPrefab);
    //         Transform asteroidTransform = asteroidObj.transform;
    //         asteroidTransform.parent = this.transform;

    //         asteroidTransform.localScale = Vector3.one * Random.Range(0.4f, 0.6f);
    //         float randX = ((idx - 2) * 2.0f) + Random.Range(0.0f, 1.8f);
    //         float randY = Random.Range(-1.5f, 1.5f);
    //         asteroidTransform.localPosition = new Vector3(randX, randY, 2);

    //         //asteroidObj.SetActive(false);
    //         asteroidObj.name = "Asteroid BG 2 [" + idx + "]";
    //         asteroidBackgroundPool2.Add(asteroidObj);
    //     }

    //     lastBgAsteroidIndex2 = asteroidBGCount2 - 1;

    //     // obstacles
    //     for (int idx = 0; idx < asteroidMaxCount; ++idx)
    //     {
    //         GameObject asteroidObj = Instantiate(asteroidPrefab);
    //         Transform asteroidTransform = asteroidObj.transform;
    //         asteroidTransform.parent = this.transform;

    //         asteroidTransform.localScale = Vector3.one * 0.8f;
    //         asteroidTransform.localPosition = new Vector3(idx * spacing, Random.Range(-2.5f, 2.5f), 0);

    //         //asteroidObj.SetActive(false);
    //         asteroidObj.name = "Asteroid [" + idx + "]";
    //         asteroidPool.Add(asteroidObj);
    //     }

    //     lastAsteroidIndex = asteroidMaxCount - 1;
    // }

    // void Update()
    // {
    //     for (int idx = 0; idx < asteroidMaxCount; ++idx)
    //     {
    //         Transform t = asteroidPool[idx].transform;
    //         Vector3 position = t.position;
    //         position.x -= Time.deltaTime * speed;
    //         t.position = position;

    //         if (position.x <= vanishingPoint.x)
    //         {
    //             Respawn(idx, Layer.Midground);
    //         }
    //     }

    //     for (int idx = 0; idx < asteroidBGCount; ++idx)
    //     {
    //         Transform t = asteroidBackgroundPool[idx].transform;
    //         Vector3 position = t.position;
    //         position.x -= Time.deltaTime * speed * 0.2f;
    //         t.position = position;

    //         if (position.x <= vanishingPoint.x)
    //         {
    //             Respawn(idx, Layer.Background);
    //         }
    //     }

    //     for (int idx = 0; idx < asteroidBGCount2; ++idx)
    //     {
    //         Transform t = asteroidBackgroundPool2[idx].transform;
    //         Vector3 position = t.position;
    //         position.x -= Time.deltaTime * speed * 0.4f;
    //         t.position = position;

    //         if (position.x <= vanishingPoint.x)
    //         {
    //             Respawn(idx, Layer.Background2);
    //         }
    //     }
    // }

    // #region Private

    // private void Respawn(int index, Layer layer)
    // {
    //     switch (layer)
    //     {
    //         case Layer.Background:
    //             {
    //                 Debug.Log("bg index = " + index);

    //                 Transform lastAsteroid = asteroidBackgroundPool[lastBgAsteroidIndex].transform;
    //                 Vector3 lastAsteroidPosition = lastAsteroid.position;

    //                 float randX = lastAsteroidPosition.x + Random.Range(0.2f, 2.0f);
    //                 float randY = Random.Range(0.0f, 1.0f);
    //                 if (index % 2 == 0) randY *= -1.0f;
    //                 lastAsteroidPosition = new Vector3(randX, randY, 1);

    //                 Transform respawnAsteroid = asteroidBackgroundPool[index].transform;
    //                 respawnAsteroid.position = lastAsteroidPosition;

    //                 if ((index + 1) % asteroidBGClusterCount == 0)
    //                 {
    //                     lastBgAsteroidIndex = index;
    //                     Debug.Log("update bg last index = " + lastBgAsteroidIndex);
    //                 }

    //                 break;
    //             }

    //         case Layer.Background2:
    //             {
    //                 Transform lastAsteroid = asteroidBackgroundPool2[lastBgAsteroidIndex2].transform;
    //                 Vector3 lastAsteroidPosition = lastAsteroid.position;

    //                 Transform respawnAsteroid = asteroidBackgroundPool2[index].transform;
    //                 float randX = lastAsteroidPosition.x + Random.Range(0.2f, 2.0f);
    //                 float randY = Random.Range(-1.5f, 1.5f);
    //                 lastAsteroidPosition = new Vector3(randX, randY, 1);

    //                 respawnAsteroid.position = lastAsteroidPosition;
    //                 lastBgAsteroidIndex2 = index;

    //                 break;
    //             }

    //         case Layer.Foreground:
    //             {
    //                 break;
    //             }

    //         default:
    //             {
    //                 Transform lastAsteroid = asteroidPool[lastAsteroidIndex].transform;
    //                 Vector3 lastAsteroidPosition = lastAsteroid.position;

    //                 Transform respawnAsteroid = asteroidPool[index].transform;
    //                 lastAsteroidPosition.x += spacing;
    //                 lastAsteroidPosition.y = Random.Range(-2.5f, 2.5f);

    //                 respawnAsteroid.position = lastAsteroidPosition;
    //                 lastAsteroidIndex = index;

    //                 break;
    //             }
    //     }
    // }

    // #endregion
}
