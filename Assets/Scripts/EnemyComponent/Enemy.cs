using UnityEngine;

namespace EnemyComponent
{
    [RequireComponent(typeof(EnemyAnimationController))]
    [RequireComponent(typeof(HealthBehavior))]
    [RequireComponent(typeof(CharacteristicsEnemy))]
    public class Enemy : MonoBehaviour
    {
        public Transform PositionHealthBar => _positionHealthBar;
        public EnemyAnimationController EnemyAnimationController => _enemyAnimationController;
        public CharacteristicsEnemy CharacteristicsEnemy => _characteristicsEnemy;
        public HealthBehavior HealthBehavior => _healthBehavior;

        [SerializeField]
        private Transform _positionHealthBar;
        [SerializeField]
        private EnemyAnimationController _enemyAnimationController;
        [SerializeField]
        private HealthBehavior _healthBehavior;
        [SerializeField]
        private CharacteristicsEnemy _characteristicsEnemy;
    }
}
