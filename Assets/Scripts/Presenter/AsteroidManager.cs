using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField]
    private GameObject asteroidPrefab;

    private List<GameObject> asteroidPool = new List<GameObject>();
    private List<GameObject> asteroidForegroundPool = new List<GameObject>();
    private List<GameObject> asteroidBackgroundPool = new List<GameObject>();

    private int asteroidMaxCount = 8;
    private int asteroidFGCount = 8;
    private int asteroidBGCount = 8;

    private Vector3 vanishingPoint;
    private int lastAsteroidIndex;

    [SerializeField]
    private float speed = 3.0f;
    private float spacing = 5.0f;

    void Start()
    {
        Camera camera = Camera.main;
        vanishingPoint = camera.ViewportToWorldPoint(Vector3.zero);
        vanishingPoint.z = 0;
        vanishingPoint.x -= 5.0f;

        // background

        // foreground

        // obstacles
        for (int idx = 0; idx < asteroidMaxCount; ++idx)
        {
            GameObject asteroidObj = Instantiate(asteroidPrefab);
            Transform asteroidTransform = asteroidObj.transform;
            asteroidTransform.parent = this.transform;

            asteroidTransform.localScale = Vector3.one * 0.8f;
            asteroidTransform.localPosition = new Vector3(idx * spacing, Random.Range(-2.5f, 2.5f), 0);

            //asteroidObj.SetActive(false);
            asteroidObj.name = "Asteroid [" + idx + "]";
            asteroidPool.Add(asteroidObj);
        }

        lastAsteroidIndex = asteroidMaxCount - 1;
    }

    void Update()
    {
        for (int idx = 0; idx < asteroidMaxCount; ++idx)
        {
            Transform t = asteroidPool[idx].transform;
            Vector3 position = t.position;
            position.x -= Time.deltaTime * speed;
            t.position = position;

            if (position.x <= vanishingPoint.x)
            {
                Respawn(idx);
            }
        }
    }

    #region Private

    private void Respawn(int index)
    {
        Transform lastAsteroid = asteroidPool[lastAsteroidIndex].transform;
        Vector3 lastAsteroidPosition = lastAsteroid.position;

        Transform respawnAsteroid = asteroidPool[index].transform;
        lastAsteroidPosition.x += spacing;
        lastAsteroidPosition.y = Random.Range(-2.5f, 2.5f);

        respawnAsteroid.position = lastAsteroidPosition;
        lastAsteroidIndex = index;
    }

    #endregion
}
