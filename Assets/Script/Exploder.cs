using System;
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

    private IExploder _targetExploder;
    [SerializeField] private Component _exploder;

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

        for(int i = 0; i < hits; i++)
        {
            var target = _buffer[i].transform.gameObject;
            var ent = target.GetComponentInParent<GameActor>();

            var distance = Vector3.Distance(target.transform.position, data.Position);
            var multiplier = Mathf.Clamp(1.15f - distance / data.Radius, 0.1f, 1f);
            var finalDamage = (int)(multiplier * data.Damage);
            ent.Life.TakeDamage(finalDamage);

            var force = data.Damage / 8f * multiplier;
            var dir = (target.transform.position - data.Position + Vector3.up).normalized;
            ent.Body.Push(dir, force);
        }

        if(hits <= 0)
            Debug.Log("EXPLODE NO HITS");

        // World.Get.SetVoxelCube(transform.position, (int)data.Radius, 0);
        World.Get.SetVoxelSphere(transform.position, (int)data.Radius, 0);
    }
}