/*  author      : brian tria
 *  date        : april 25, 2020
 *  description : 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Background image is missing.");
            return;
        }

        float width = spriteRenderer.sprite.bounds.size.x;
        float height = spriteRenderer.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 scale = Vector3.one;
        scale.x = worldScreenWidth / width;
        scale.y = worldScreenHeight / height;

        transform.localScale = scale;
    }
}
