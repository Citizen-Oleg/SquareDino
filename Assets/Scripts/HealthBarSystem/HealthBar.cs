using EnemyComponent;
using PlayerComponent;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

namespace HealthBarSystem
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Slider _healthBarSlider;

        private Player _player;
        private Enemy _enemy;
        private CharacteristicsEnemy _characteristicsEnemy;

        private void Awake()
        {
            _healthBarSlider.interactable = false;
            _player = LevelManager.Player;
        }

        private void LateUpdate()
        {
            transform.LookAt(_player.transform, Vector3.down);
        }

        public void Initialize(Enemy enemy)
        {
            _enemy = enemy;
            _characteristicsEnemy = _enemy.CharacteristicsEnemy;
            _enemy.HealthBehavior.OnHealthChange += RefreshUI;
            transform.position = _enemy.PositionHealthBar.position;

            RefreshUI();
        }

        private void RefreshUI()
        {
            if (_characteristicsEnemy != null)
            {
                _healthBarSlider.value = _characteristicsEnemy.CurrentHp / _characteristicsEnemy.MAXHp;
            }
        }

        private void OnDestroy()
        {
            if (_enemy != null)
            {
                _enemy.HealthBehavior.OnHealthChange -= RefreshUI;
            }
        }
    }
}