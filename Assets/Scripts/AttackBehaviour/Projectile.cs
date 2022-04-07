using System;
using DefaultNamespace;
using EnemyComponent;
using Event;
using Factory;
using UnityEngine;

namespace AttackBehaviour
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour, IProduct<Projectile>
    {
        public int ID => _id;

        [SerializeField]
        private int _id;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _damage;

        private Vector3 _direction;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Initialize(Vector3 direction)
        {
            _direction = direction;
            Update();
        }

        private void Update()
        {
            _rigidbody.velocity = _direction * _speed;
            transform.forward = _rigidbody.velocity;
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.TryGetComponent(out Enemy enemy))
            {
                enemy.HealthBehavior.ApplyDamage(_damage);
            }
            
            gameObject.SetActive(false);
        }
    }
}