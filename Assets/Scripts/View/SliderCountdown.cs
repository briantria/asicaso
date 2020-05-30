/* author: Brian Tria
 * created: May 25, 2020
 * description: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderCountdown : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = this.GetComponent<Slider>();

        if (slider == null)
        {
            Debug.LogError("slider missing.");
        }
    }

    void Update()
    {

    }
}
