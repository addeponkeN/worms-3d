using UnityEngine;

namespace EntityComponents
{
    public class CrateLooterComponent : BaseEntityComponent
    {
        private void OnTriggerEnter(Collider other)
        {
            var airDrop = other.gameObject.GetComponentInParent<AirDrop>();
            if(airDrop != null)
            {
                var player = gameObject.GetComponentInParent<Player>();
                airDrop.LootItem(player);
            }
        }
    }
}