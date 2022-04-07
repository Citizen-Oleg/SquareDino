using System;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyComponent
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimationController : MonoBehaviour
    {
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        
        [SerializeField]
        private List<Rigidbody> _rigidBodies = new List<Rigidbody>();

        private Animator _animator;
        private Collider _hitCollider;
        private Enemy _enemy;
        
        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
            _animator = GetComponent<Animator>();
            _hitCollider = GetComponent<Collider>();

            _enemy.HealthBehavior.OnDead += SetAnimationDead;
        }

        private void SetAnimationDead(Enemy enemy)
        {
            var isDead = enemy.CharacteristicsEnemy.IsDead;
            _animator.SetBool(IsDead, isDead);

            _hitCollider.enabled = !isDead;
            _animator.enabled = !isDead;
            foreach (var rigidbody in _rigidBodies)
            {
                rigidbody.isKinematic = !isDead;
            }
        }

        private void OnDestroy()
        {
            _enemy.HealthBehavior.OnDead -= SetAnimationDead;
        }
    }
}