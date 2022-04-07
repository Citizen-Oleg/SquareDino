using UnityEngine;

namespace PlayerComponent
{
    [RequireComponent(typeof(PlayerAnimationController))]
    [RequireComponent(typeof(PlayerMovementController))]
    public class Player : MonoBehaviour
    {
        public PlayerAnimationController PlayerAnimationController => _playerAnimationController;
        public PlayerMovementController PlayerMovementController => _playerMovementController;

        [SerializeField]
        private PlayerAnimationController _playerAnimationController;
        [SerializeField]
        private PlayerMovementController _playerMovementController;
    }
}