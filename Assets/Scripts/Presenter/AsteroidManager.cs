/* author: Brian Tria
 * created: May 08, 2020
 * description: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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

    // [SerializeField]
    // private int sortingOrder = 0;

    [SerializeField]
    private Vector2 scaleRange = Vector2.one;

    [SerializeField]
    private float verticalViewPortSpan = 1.0f;

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
    private bool isPlaying;
    private Camera mainCamera;

    #endregion

    #region LifeCycle

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

    void Awake()
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

        Vector3 verticalSpanScreen = mainCamera.ViewportToWorldPoint(new Vector3(0.0f, verticalViewPortSpan, 0.0f));
        verticalViewPortSpan = verticalSpanScreen.y;

        isPlaying = false;
    }

    void Start()
    {
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
        if (!isPlaying)
        {
            return;
        }

        float deltaPosX = Time.deltaTime * speed;
        lastClusterPosition.x -= deltaPosX;

        for (int idx = 0; idx < maxAsteroidPoolCount; ++idx)
        {
            Transform t = asteroidPool[idx].transform;
            Vector3 position = t.position;
            position.x -= deltaPosX;
            t.position = position;

            if (position.x <= vanishingPoint.x)
            {
                Spawn(idx);
            }
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
        this.Reset();
    }

    void Reset()
    {
        lastClusterPosition = spawnPoint;
        lastSpawnIndex = -1;

        for (int idx = 0; idx < maxAsteroidPoolCount; ++idx)
        {
            this.Spawn(idx);
        }
    }

    void Spawn(int index)
    {
        Vector3 lastPosition = spawnPoint;
        lastPosition.x -= horizontalSpan;

        if (lastSpawnIndex >= 0 && lastSpawnIndex < maxAsteroidPoolCount)
        {
            Transform lastTransform = asteroidPool[lastSpawnIndex].transform;
            lastPosition = lastTransform.position;
        }

        Transform asteroidTransform = asteroidPool[index].transform;
        float posX = lastPosition.x + horizontalSpan;
        float posY = Random.Range(0.1f, verticalViewPortSpan);

        if (index % 2 == 0)
        {
            posY *= -1;
        }

        if (clusterAsteroidCount > 1)
        {
            float startRangeX = lastClusterPosition.x;
            float endRangeX = startRangeX + horizontalSpan;
            posX = Random.Range(startRangeX, endRangeX);

            if (index % clusterAsteroidCount == 0)
            {
                lastClusterPosition.x += horizontalSpan;
            }
        }

        asteroidTransform.parent = this.transform;
        asteroidTransform.localScale = Vector3.one * Random.Range(scaleRange.x, scaleRange.y);
        asteroidTransform.localPosition = new Vector3(posX, posY, 0);

        lastSpawnIndex = index;
    }

    #endregion
}
