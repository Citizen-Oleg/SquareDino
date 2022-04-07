using System.Collections.Generic;
using AttackBehaviour;
using DefaultNamespace;
using Tools;
using UnityEngine;

namespace Factory
{
    public class ProjectileFactory : MonoBehaviour, IFactory<Projectile>
    {
        private const int COUNT_OBJECT_POOL = 10;
        
        [SerializeField]
        private Transform _container;
        [SerializeField]
        private List<Projectile> _projectiles = new List<Projectile>();

        private readonly Dictionary<int, ObjectPool<Projectile>> _projectileDictionary =
            new Dictionary<int, ObjectPool<Projectile>>();
        
        private void Awake()
        {
            foreach (var projectile in _projectiles)
            {
                CreatePoolProjectile(projectile);
            }
        }

        private void CreatePoolProjectile(Projectile projectile)
        {
            if (!_projectileDictionary.ContainsKey(projectile.ID))
            {
                var pool = new ObjectPool<Projectile>(projectile, _container, COUNT_OBJECT_POOL);
                _projectileDictionary.Add(projectile.ID, pool);
            }
        }

        public Projectile GetProduct(Projectile product)
        {
            if (_projectileDictionary.TryGetValue(product.ID, out var pool))
            {
                return pool.Get();
            }
            
            CreatePoolProjectile(product);
            return _projectileDictionary[product.ID].Get();

        }

        public void ReleaseProduct(Projectile product)
        {
            product.gameObject.SetActive(false);
        }
    }
}