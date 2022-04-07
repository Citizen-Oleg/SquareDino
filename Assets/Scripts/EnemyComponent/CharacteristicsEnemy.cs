using UnityEngine;

namespace EnemyComponent
{
    public class CharacteristicsEnemy : MonoBehaviour
    {
        public float MAXHp
        {
            get => _maxHP;
            set => _maxHP = value;
        }

        public float CurrentHp
        {
            get => _currentHP;
            set => _currentHP = Mathf.Clamp(value, 0, _maxHP);
        }

        public bool IsDead { get; set; }

        [SerializeField]
        private float _maxHP;
        [SerializeField]
        private float _currentHP;
    }
}