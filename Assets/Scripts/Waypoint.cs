using System;
using System.Collections.Generic;
using EnemyComponent;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public event Action<Waypoint> OnWaypointCleared;

    public Transform BattlePoint => _battlePoint;
    public bool IsFinish => _nextWaypoint == null;

    [SerializeField]
    private Waypoint _nextWaypoint;
    [SerializeField]
    private Transform _battlePoint;
    [SerializeField]
    private List<Enemy> _waypointEnemies = new List<Enemy>();

    private int _numberEnemyKilled;

    private void Awake()
    {
        foreach (var enemy in _waypointEnemies)
        {
            enemy.HealthBehavior.OnDead += EnemyClearCheck;
        }
    }

    public bool CheckEveryoneDead()
    {
        var everyoneDead = _waypointEnemies.TrueForAll(enemy => enemy.CharacteristicsEnemy.IsDead);
        if (everyoneDead && _nextWaypoint != null)
        {
            OnWaypointCleared?.Invoke(_nextWaypoint);
            return true;
        }

        return false;
    }

    private void EnemyClearCheck(Enemy enemy)
    {
        if (++_numberEnemyKilled != _waypointEnemies.Count)
        {
            return;
        }
        
        if (_nextWaypoint != null)
        {
            OnWaypointCleared?.Invoke(_nextWaypoint);
        }
    }

    private void OnDestroy()
    {
        foreach (var enemy in _waypointEnemies)
        {
            enemy.HealthBehavior.OnDead -= EnemyClearCheck;
        }
    }
}
