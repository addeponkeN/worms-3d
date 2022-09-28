using System;
using AudioSystem;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    public static GameCore Get { get; private set; }

    private void Awake()
    {
        Debug.Log("loading core");
        if(Get == null)
        {
            Get = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        AudioManager.Load();
        Debug.Log("core loaded");
    }
}