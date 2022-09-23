// using System;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
//
// namespace EntityComponents
// {
//     public class ComponentManager : MonoBehaviour
//     {
//         public List<BaseEntityComponent> Components;
//
//         [NonSerialized] public GameActor Ent;
//         
//         public T GetGameComponent<T>() where T : BaseEntityComponent => (T)Components.FirstOrDefault(x => x is T) as T;
//
//         private void Awake()
//         {
//             Components = new List<BaseEntityComponent>();
//             Ent = GetComponent<GameActor>();
//         }
//
//         private void Start()
//         {
//             AddGameComponent(new SimpleBody(Ent.CharController));
//         }
//
//         public void AddGameComponent(BaseEntityComponent comp)
//         {
//             comp.Ent = Ent;
//             comp.Init();
//             Components.Add(comp);
//         }
//
//         private void Update()
//         {
//             for(int i = 0; i < Components.Count; i++)
//             {
//                 Components[i].Update();
//             }
//         }
//
//         private void FixedUpdate()
//         {
//             for(int i = 0; i < Components.Count; i++)
//             {
//                 Components[i].FixedUpdate();
//             }
//         }
//     }
// }