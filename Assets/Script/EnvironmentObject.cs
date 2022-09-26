using System;
using UnityEngine;

[RequireComponent(typeof(ExplosionTargetObject))]
public class EnvironmentObject : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<ExplosionTargetObject>().DamagedEvent += OnDamagedEvent;
    }

    private void OnDamagedEvent(ExplodeData obj)
    {
        Destroy(gameObject);
    }
}