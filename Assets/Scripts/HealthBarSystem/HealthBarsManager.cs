using System.Collections.Generic;
using EnemyComponent;
using UnityEngine;

namespace HealthBarSystem
{
    public class HealthBarsManager : MonoBehaviour
    {
        [SerializeField]
        private HealthBar _healthBar;
        [SerializeField]
        private RectTransform _container;
        [SerializeField]
        private List<Enemy> _enemies = new List<Enemy>();

        private readonly Dictionary<Enemy, HealthBar> _enemyHealthBar = new Dictionary<Enemy, HealthBar>();

        private void Awake()
        {
            foreach (var enemy in _enemies)
            {
                CreateHealthBar(enemy);
                enemy.HealthBehavior.OnDead += DestroyHealthBar;
            }
        }

        private void CreateHealthBar(Enemy enemy)
        {
            var healthBar = Instantiate(_healthBar, _container);
            healthBar.Initialize(enemy);
            _enemyHealthBar.Add(enemy, healthBar);
        }

        private void DestroyHealthBar(Enemy enemy)
        {
            if (_enemyHealthBar.ContainsKey(enemy))
            {
                Destroy(_enemyHealthBar[enemy].gameObject);
                _enemyHealthBar.Remove(enemy);
            }
        }

        private void OnDestroy()
        {
            foreach (var enemy in _enemies)
            {
                enemy.HealthBehavior.OnDead -= DestroyHealthBar;
            }
        }
    }
}