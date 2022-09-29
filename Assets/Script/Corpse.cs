using GameStates;
using UnityEngine;
using Util;

public class Corpse : MonoBehaviour, IFollowable
{
    public bool EndFollow { get; set; } = true;
    public Transform Transform => transform;

    private Timer _life = 4f;
    
    private void Update()
    {
        if(_life.CheckUpdate())
        {
            Destroy(gameObject);
        }
    }
}