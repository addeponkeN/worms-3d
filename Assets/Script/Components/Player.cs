using System;
using Teams;
using Ui;
using UnityEngine;
using ProgressBar = Ui.Widgets.ProgressBar;

namespace Components
{
    public class Player : GameActor
    {
        [NonSerialized] public PlayerUi Ui;
        public Transform CameraPosition;
        public Transform CameraAimPosition;
        public Transform WeaponOrigin;

        private Team _team;
        private int _playerId;
        private ProgressBar _bar;

        protected override void Awake()
        {
            base.Awake();
            CharController = GetComponent<CharacterController>();
            Ui = gameObject.AddComponent<PlayerUi>();
        }

        public Team GetTeam() => _team;
        public int GetPlayerId() => _playerId;

        public void AssignTeam(Team team)
        {
            _team = team;
            _playerId = team.Players.Count;
            Ui.Init(this);
        }

        protected override void OnExplosionEvent(ExplodeData data)
        {
            base.OnExplosionEvent(data);

            var distance = Vector3.Distance(transform.position, data.Position);
            var multiplier = Mathf.Clamp(1.15f - distance / data.Radius, 0.1f, 1f);
            var finalDamage = (int)(multiplier * data.Damage);
            Life.TakeDamage(finalDamage);

            Debug.Log($"player took dmg from explo: {finalDamage}");

            var force = data.Damage / 10f * multiplier;
            var dir = ((transform.position - data.Position).normalized + Vector3.up).normalized;
            Body.Push(dir, force);
        }
    }
}