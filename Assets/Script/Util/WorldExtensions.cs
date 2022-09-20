using UnityEngine;
using VoxelEngine;

namespace Util
{
    public static class WorldExtensions
    {
        public static Vector3 GetRandomSafePosition(this World world)
        {
            return GetRandomSafePosition(world, 100);
        }

        //  bruteforce, bad, works for now.
        public static Vector3 GetRandomSafePosition(this World world, int tries)
        {
            Vector3 pos;
            var chunks = world.GetChunkList();

            var cs = (float)World.ChunkSize.x;

            do
            {
                tries--;
                if(tries < 0)
                {
                    Debug.LogError("COULD NOT FIND RANDOM SAFE POSITION");
                    return Vector3.zero;
                }

                var chunkPos = chunks.Random().ChunkGo.transform.position;
                
                pos = new Vector3(
                    chunkPos.x + Random.Range(-cs, cs),
                    World.ChunkSize.y + 1,
                    chunkPos.x + Random.Range(-cs, cs));
                
            } while(!IsPositionSafe(world, ref pos));

            return pos;
        }

        static bool IsPositionSafe(World world, ref Vector3 pos)
        {
            var ray = new Ray(pos, Vector3.down);

            if(Physics.Raycast(ray, out var info))
            {
                if(info.point.y > world.WaterLevel)
                {
                    pos = new Vector3(info.point.x, info.point.y + 1, info.point.z);
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}