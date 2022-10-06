using AudioSystem;
using UnityEngine;
using Util;
using Random = UnityEngine.Random;

public class Explosion : MonoBehaviour
{
    private Timer _lifeTimer;

    private void Awake()
    {
        var particle = GetComponent<ParticleSystem>();
        _lifeTimer = particle.main.duration + 1f;
        AudioManager.PlaySfx($"explosion{Random.Range(1, 4)}");
    }

    private void Update()
    {
        if(_lifeTimer.UpdateCheck())
        {
            Destroy(gameObject);
        }
    }
}