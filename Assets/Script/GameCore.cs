using System;
using AudioSystem;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    public static GameCore Get { get; private set; }

    [NonSerialized] public AudioManager Aud;

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

        Aud = gameObject.AddComponent<AudioManager>();
        Debug.Log("core loaded");
    }
}