using AudioSystem;
using Ui;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    public static GameCore Get { get; private set; }

    public static bool IsInited => Get != null;
    
    public GameRulesInfo GameRules;

    private void Awake()
    {
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


        GameRules = new GameRulesInfo();
        PrefabManager.Load();
        AudioManager.Load();
        
    }
}