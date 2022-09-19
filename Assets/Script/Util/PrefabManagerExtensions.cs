using Projectiles;
using UnityEngine;
using Weapons;

namespace Util
{
    public static class PrefabManagerExtensions
    {
        public static GameObject GetPrefab(this PrefabManager pm, ProjectileTypes projectileType)
        {
            return pm.GetPrefab($"projectile_{projectileType.ToString().ToLowerInvariant()}");
        }

        public static GameObject GetPrefab(this PrefabManager pm, WeaponTypes weaponType)
        {
            return pm.GetPrefab($"weapon_{weaponType.ToString().ToLowerInvariant()}");
        }
    }
}