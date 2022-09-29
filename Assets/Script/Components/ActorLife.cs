using System;
using UnityEngine;

namespace Components
{
    public class ActorLife
    {
        public GameActor Parent;

        public bool IsAlive => Life > 0;
        public int Life { get; set; }
        public int MaxLife { get; set; }

        public float LifePercentage => (float)Life / MaxLife;

        /// <summary>
        /// EntityLife(life), int(damage)
        /// </summary>
        public event Action<ActorLife, int> LifeChangedEvent;

        public event Action<GameActor> DeathEvent;

        public ActorLife(GameActor actor, int life)
        {
            Parent = actor;
            Life = life;
            MaxLife = life;
        }

        public void TakeDamage(int dmg)
        {
            if(dmg == 0)
            {
                return;
            }

            Life -= dmg;
            Debug.Log($"took dmg: {dmg}  ({Life}hp)");
            LifeChangedEvent?.Invoke(this, -dmg);
            if(!IsAlive)
            {
                DeathEvent?.Invoke(Parent);
            }
        }

        public void Heal(int val)
        {
            if(val == 0)
            {
                return;
            }

            Life = Mathf.Clamp(Life + val, 0, MaxLife);
            Debug.Log($"heal: +{val}  ({Life}hp)");
            LifeChangedEvent?.Invoke(this, val);
        }

        public void Kill()
        {
            Life = 0;
            DeathEvent?.Invoke(Parent);
        }
    }
}