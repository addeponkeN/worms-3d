using System;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    private RaycastHit[] _hitBuffer;

    private void Awake()
    {
        _hitBuffer = new RaycastHit[10];
    }

    public void RegisterExploder()
    {
        
    }

    public void Explode(Vector3 pos, float radius, int damage)
    {
        
    }
    
}