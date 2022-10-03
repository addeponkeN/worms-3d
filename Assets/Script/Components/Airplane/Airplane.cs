using System;
using System.Collections;
using GameStates;
using Projectiles;
using UnityEngine;
using Util;
using Random = UnityEngine.Random;

namespace Components
{
    public class Airplane : MonoBehaviour, IFollowable
    {
        public bool EndFollow { get; set; }
        public Transform Transform => transform;

        public event Action BombsDroppedEvent;

        public float Speed = 2f;
        public int BombCount = 5;

        private Transform _bombSpawnTf;
        private bool _startedDropping;
        private float _distanceTravelled;
        private Vector3 _strikePosition;
        private PlanePathInfo _info;

        private void Awake()
        {
            _bombSpawnTf = transform.Find("BombPosition");
        }

        public void SetStrikePosition(Vector3 pos)
        {
            _strikePosition = pos;
            _info = GeneratePath();
            transform.position = _info.StartPosition;
            transform.rotation = Quaternion.LookRotation(_info.Direction);
        }

        private PlanePathInfo GeneratePath()
        {
            const float offsetDistance = 200f;
            var positionOffsetDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;

            var startPosition = _strikePosition + positionOffsetDirection * offsetDistance;

            var flyDirection = (new Vector2(_strikePosition.x, _strikePosition.z) -
                                new Vector2(startPosition.x, startPosition.z)).normalized;

            startPosition.y = 30f;

            return new PlanePathInfo(startPosition, new Vector3(flyDirection.x, 0, flyDirection.y));
        }

        private bool IsCloseEnoughToDropBombs()
        {
            var pos = transform.position;
            return Vector2.Distance(new Vector2(pos.x, pos.z), new Vector2(_strikePosition.x, _strikePosition.z)) < 20f;
        }

        private IEnumerator DropBombs()
        {
            while(BombCount-- > 0)
            {
                Instantiate(PrefabManager.GetPrefab(ProjectileTypes.Bomb),
                    _bombSpawnTf.position,
                    Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void Update()
        {
            var totSpeed = Speed * Time.deltaTime;
            var move = _info.Direction * totSpeed;

            transform.position += move;
            _distanceTravelled += totSpeed;

            if(!_startedDropping && IsCloseEnoughToDropBombs())
            {
                _startedDropping = true;
                BombsDroppedEvent?.Invoke();
                StartCoroutine(DropBombs());
            }

            if(_distanceTravelled > 350f)
            {
                EndFollow = true;
                Destroy(gameObject);
            }
        }
    }
}