using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesHandler : MonoBehaviour
{
    [SerializeField] private float sunEnergyTick = 5f;

    private void OnParticleCollision(GameObject other)
    {
        GameManager.Instance.AddResources(sunEnergyTick, 0f);
    }
}
