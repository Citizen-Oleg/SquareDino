using System;
using UnityEngine;

namespace PlayerComponent
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationController : MonoBehaviour
    {
        private static readonly int IsRun = Animator.StringToHash("IsRun");

        private Animator _animator;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetRunAnimation(bool isRun)
        {
            _animator.SetBool(IsRun, isRun);
        }
    }
}