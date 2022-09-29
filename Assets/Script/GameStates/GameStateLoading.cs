using System.Collections.Generic;
using UnityEngine;
using VoxelEngine;

namespace GameStates
{
    public interface ILoader
    {
        void Load();
    }

    public class GameStateLoading : GameState
    {
        private Queue<ILoader> _loaders;

        public GameStateLoading()
        {
            _loaders = new Queue<ILoader>();
        }

        public override void Init(GameStateManager manager)
        {
            base.Init(manager);

            var game = GameManager.Get;

            //  setup world
            var prefWorld = PrefabManager.Get.GetPrefab("voxelworld");
            var worldGo = Object.Instantiate(prefWorld);
            var world = worldGo.GetComponent<World>();

            _loaders.Enqueue(world);

            world.UpdateChunkMeshes();

            //  setup game systems
            game.SetupManagers();

            _loaders.Enqueue(game.CamManager);
            _loaders.Enqueue(game.PlayerManager);
            _loaders.Enqueue(game.Ui);
        }

        public override void Update()
        {
            base.Update();
            if(_loaders.Count > 0)
            {
                var loader = _loaders.Dequeue();
                loader.Load();
            }
            else
            {
                Exit();
            }
        }

        public override void Exit()
        {
            base.Exit();
            var systems = GameManager.Get.Systems;
            for(int i = 0; i < systems.Count; i++)
                systems[i].OnGameStarted();
        }
    }
}