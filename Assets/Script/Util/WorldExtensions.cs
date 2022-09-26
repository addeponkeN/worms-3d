using UnityEngine;
using VoxelEngine;

namespace Util
{
    public static class WorldExtensions
    {
        public static Vector3 GetRandomSafePosition(this World world)
        {
            return GetRandomSafePosition(world, 20);
        }

        //  bruteforce, bad, works for now.
        public static Vector3 GetRandomSafePosition(this World world, int tries)
        {
            var chunks = world.GetChunkList();
            float chunkRange = World.ChunkSize.x - 1f;
            Vector3 position = Vector3.zero;
            do
            {
                tries--;
                if(tries <= 0)
                {
                    Debug.LogError("failed to find a spawn position");
                    break;
                }

                var chunkPos = chunks.Random().ChunkGo.transform.position;
                
                position = new Vector3(
                    chunkPos.x + Random.Range(-chunkRange, chunkRange),
                    World.ChunkSize.y,
                    chunkPos.z + Random.Range(-chunkRange, chunkRange));
                
            } while(!world.IsPositionSafe(ref position));
            
            return position;
        }

        public static bool IsPositionSafe(this World world, ref Vector3 pos)
        {
            int layer = 1 << LayerMask.NameToLayer("World");
            const float distance = 9999f;
            var ray = new Ray(pos, Vector3.down);

            if(Physics.Raycast(ray, out var info, distance, layer))
            {
                if(info.point.y > world.Water.WaterLevel)
                {
                    pos = new Vector3(info.point.x, info.point.y, info.point.z);
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}