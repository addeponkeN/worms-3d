using UnityEngine;
using VoxelEngine;

namespace EntityComponents
{
    public class LifeBoundsComponent : BaseEntityComponent
    {
        public override void Update()
        {
            base.Update();

            float waterLevel = World.Get.Water.WaterLevel;
            if(Ent.transform.position.y < waterLevel)
            {
                Ent.Life.Kill();
                Debug.Log("killed by bounds");
            }
        }
    }
}