using Factory;
using PlayerComponent;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AttackBehaviour
{
    public class Gun : MonoBehaviour
    {
        [SerializeField]
        private Player _player;
        [SerializeField]
        private Transform _muzzlePosition;
        [SerializeField]
        private Projectile _projectile;

        private Camera _camera;
        private ProjectileFactory _projectileFactory;

        private void Start()
        {
            _camera = Camera.main;
            _projectileFactory = LevelManager.ProjectileFactory;
        }

        private void Update()
        {
            if (_player.PlayerMovementController.IsRun)
            {
                return;
            }
            
            #if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Shoot(Input.mousePosition);
            }
            #endif
            
            if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Touch touch = Input.GetTouch(0);
                
                if (touch.phase == TouchPhase.Began)
                {
                    Shoot(touch.position);
                }
            }
        }

        private void Shoot(Vector2 position)
        {
            var ray = _camera.ScreenPointToRay(position);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                var projectile = _projectileFactory.GetProduct(_projectile);

                projectile.Initialize(Vector3.Normalize(hitInfo.point - _muzzlePosition.position));
                
                projectile.transform.position = _muzzlePosition.position;
                projectile.transform.rotation = _muzzlePosition.rotation;
            }
        }
    }
}