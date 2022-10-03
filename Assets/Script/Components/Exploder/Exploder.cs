using System;
using System.Collections.Generic;
using AudioSystem;
using UnityEngine;
using Util;
using VoxelEngine;
using Random = UnityEngine.Random;

namespace Components
{
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
                throw new Exception("exploder is null");
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

            var hash = new HashSet<EnvironmentInteractor>();
        
            for(int i = 0; i < hits; i++)
            {
                var target = _buffer[i].transform.gameObject;
                var interactor = target.GetComponent<EnvironmentInteractor>();
                if(interactor == null)
                    continue;

                if(hash.Contains(interactor))
                    continue;
            
                hash.Add(interactor);
            
                interactor.OnTriggerExplosion(data);
            }

            //  temp random explosion sfx
            AudioManager.PlaySfx($"explosion{Random.Range(1, 4)}");
        
            World.Get.SetVoxelsSphere(transform.position, (int)data.Radius, 0);
        }
    }
}