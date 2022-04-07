using System;
using UnityEngine;

namespace EnemyComponent
{
    [RequireComponent(typeof(Enemy))]
    public class HealthBehavior : MonoBehaviour
    {
        public event Action<Enemy> OnDead;
        public event Action OnHealthChange;

        private Enemy _enemy;
        
        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
        }

        public void ApplyDamage(float damage)
        {
            _enemy.CharacteristicsEnemy.CurrentHp -= damage;
            OnHealthChange?.Invoke();

            if (_enemy.CharacteristicsEnemy.CurrentHp <= 0)
            {
                _enemy.CharacteristicsEnemy.IsDead = true;
                OnDead?.Invoke(_enemy);
            }
        }
    }
}