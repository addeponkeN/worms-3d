using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class EnvironmentInteractor : MonoBehaviour
{
    public event Action<ExplodeData> ExplosionEvent;

    private void Awake()
    {
        var box = GetComponent<BoxCollider>();
        box.isTrigger = true;

        var body = GetComponent<Rigidbody>();
        body.isKinematic = true;
        
        gameObject.layer = LayerMask.NameToLayer("Damageable");
    }

    public void OnTriggerExplosion(ExplodeData data)
    {
        ExplosionEvent?.Invoke(data);
    }
    
}