using AudioSystem;
using VoxelEngine;

namespace Components
{
    public class LifeBoundsComponent : BaseEntityComponent
    {
        protected override void Update()
        {
            base.Update();

            float waterLevel = World.Get.Water.WaterLevel;
            if(Actor.transform.position.y < waterLevel)
            {
                //  killed by water level
                Actor.Life.Kill();
                AudioManager.PlaySfx("watersplash");
            }
        }
    }
}