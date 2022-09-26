using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class ExplosionTargetObject : MonoBehaviour
{
    public event Action<ExplodeData> DamagedEvent;

    public GameObject DamageableGameObject;
    
    private void Awake()
    {
        var box = GetComponent<BoxCollider>();
        box.isTrigger = true;

        var body = GetComponent<Rigidbody>();
        body.isKinematic = true;
        
        gameObject.layer = LayerMask.NameToLayer("Damageable");
    }

    public void OnTriggerDamageable(ExplodeData data)
    {
        DamagedEvent?.Invoke(data);
    }
    
}