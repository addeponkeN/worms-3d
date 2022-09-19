using System.Collections.Generic;
using System.Linq;

namespace PlayerControllers
{
    public class ControllerSet : BasePlayerController
    {
        public List<BasePlayerController> Controllers;

        public ControllerSet()
        {
            Controllers = new List<BasePlayerController>();
        }
        
        public void AddController(BasePlayerController controller)
        {
            controller.IsAlive = true;
            controller.Manager = Manager;
            if(Manager.Started)
                controller.Init();
            Controllers.Add(controller);
        }
        
        public T GetController<T>() where T : BasePlayerController 
            => Controllers.FirstOrDefault(x => x is T) as T;

        public override void Init()
        {
            for(int i = 0; i < Controllers.Count; i++)
            {
                Controllers[i].Init();
            }
        }

        public override void Update()
        {
            for(int i = 0; i < Controllers.Count; i++)
            {
                Controllers[i].Update();
            }
        }

        public override void FixedUpdate()
        {
            for(int i = 0; i < Controllers.Count; i++)
            {
                Controllers[i].FixedUpdate();
            }
        }

        public void LateUpdate()
        {
            for(int i = 0; i < Controllers.Count; i++)
            {
                if(!Controllers[i].IsAlive)
                    Controllers.RemoveAt(i--);
            }
        }
        
    }
}