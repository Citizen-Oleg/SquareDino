using System;
using Event;
using Factory;
using PlayerComponent;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public static ProjectileFactory ProjectileFactory => instance._projectileFactory;
    public static Player Player => instance._player;

    [SerializeField]
    private ProjectileFactory _projectileFactory;
    [SerializeField]
    private Player _player;
    
    private IDisposable _subscription;

    protected override void Awake()
    {
        base.Awake();

        _subscription = EventStreams.UserInterface.Subscribe<RestartGameEvent>(RestartGame);
    }

    private void RestartGame(RestartGameEvent restartGameEvent)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        
        _subscription?.Dispose();
    }
}