using VoxelEngine;

namespace EntityComponents
{
    public class LifeBoundsComponent : BaseEntityComponent
    {
        public override void Update()
        {
            base.Update();

            float waterLevel = World.Get.WaterLevel;

            if(Ent.transform.position.y < waterLevel)
            {
                //  kill
                Ent.Life.Kill();
            }
            
        }
    }
}