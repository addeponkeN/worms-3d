using Projectiles;
using UnityEngine;
using Weapons;

public static partial class PrefabManager
{
    public static GameObject GetPrefab(ProjectileTypes projectileType)
    {
        return global::PrefabManager.GetPrefab($"projectile_{projectileType.ToString().ToLowerInvariant()}");
    }

    public static GameObject GetPrefab(WeaponTypes weaponType)
    {
        return global::PrefabManager.GetPrefab($"weapon_{weaponType.ToString().ToLowerInvariant()}");
    }
}