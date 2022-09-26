using System;
using System.Collections.Generic;
using UnityEngine;
using VoxelEngine;

public interface IExploder
{
    public event Action<ExplodeData> ExplodeEvent;
}

public struct ExplodeData
{
    public Vector3 Position;
    public float Radius;
    public int Damage;

    public ExplodeData(Vector3 position, float radius, int damage)
    {
        Position = position;
        Radius = radius;
        Damage = damage;
    }
}

public class Exploder : MonoBehaviour
{
    private static RaycastHit[] _buffer = new RaycastHit[16];

    [SerializeField] private Component _exploder;

    private IExploder _targetExploder;
    private int _layer;

    private void Awake()
    {
        if(_exploder == null)
        {
            throw new Exception("Exploder.Target is null");
        }

        if(_exploder is IExploder exp)
        {
            _targetExploder = exp;
        }
        else
        {
            throw new Exception("Exploder.Target is not of type IExploder");
        }

        _layer = 1 << LayerMask.NameToLayer("Damageable");

        _targetExploder.ExplodeEvent += TargetExploder_OnExplodeEvent;
    }

    private void TargetExploder_OnExplodeEvent(ExplodeData data)
    {
        _targetExploder.ExplodeEvent -= TargetExploder_OnExplodeEvent;

        var ray = new Ray(data.Position, Vector3.up);

        var hits = Physics.SphereCastNonAlloc(ray, data.Radius, _buffer, 1f, _layer);

        Debug.Log($"{hits} explosion hits");

        for(int i = 0; i < hits; i++)
        {
            var target = _buffer[i].transform.gameObject;
            var exploTarget = target.GetComponent<ExplosionTargetObject>();
            if(exploTarget == null)
            {
                Debug.LogError($"explotarget null!  go: {target.name}");
            }

            exploTarget.OnTriggerDamageable(data);
            Debug.Log(target.name + " hit");
        }

        World.Get.SetVoxelsSphere(transform.position, (int)data.Radius, 0);
    }
}