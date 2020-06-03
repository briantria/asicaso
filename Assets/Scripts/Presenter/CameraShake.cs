// source: https://gist.github.com/ftvs/5822103

using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float shakeDuration = 0.5f;

    [SerializeField]
    private float shakeAmount = 0.7f;

    private float remainingTime = 0f;
    private float decreaseFactor = 1.0f;
    private Transform camTransform;
    private bool damaged = false;
    private Vector3 originalPos;

    #region Life Cycle 

    void OnEnable()
    {
        LifePointSystem.OnDamage += Shake;
    }

    void OnDisable()
    {
        LifePointSystem.OnDamage -= Shake;
    }

    void Awake()
    {
        camTransform = this.transform;
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (!damaged)
        {
            return;
        }

        if (remainingTime > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            remainingTime -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            remainingTime = 0f;
            camTransform.localPosition = originalPos;
            damaged = false;
        }
    }

    #endregion

    #region Private

    void Shake(int damage)
    {
        originalPos = camTransform.localPosition;
        remainingTime = shakeDuration;
        damaged = true;
    }

    #endregion
}