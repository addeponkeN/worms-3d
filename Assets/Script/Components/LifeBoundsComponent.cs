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
                Actor.Life.Kill();
            }
        }
    }
}