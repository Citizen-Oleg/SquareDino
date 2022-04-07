using System;
using System.Collections;
using Event;
using UnityEngine;
using UnityEngine.AI;

namespace PlayerComponent
{
    [RequireComponent(typeof(Player))]
    public class PlayerMovementController : MonoBehaviour
    {
        private const float DISTANCE_TO_POINT = 0.5f;

        public bool IsRun { get; private set; }

        [SerializeField]
        private Waypoint _startWaypoint;
        [SerializeField]
        private NavMeshAgent _navMeshAgent;
        [Space]
        [SerializeField]
        private float _timeRotaion;

        private Player _player;
        private Waypoint _currentWaypoint;
        private IDisposable _subscription;

        private void Awake()
        {
            _subscription = EventStreams.UserInterface.Subscribe<StartGameEvent>(MoveToWaypoint);
            _player = GetComponent<Player>();
        }
        
        private void MoveToWaypoint()
        {
            _navMeshAgent.SetDestination(_currentWaypoint.BattlePoint.position);
            _player.PlayerAnimationController.SetRunAnimation(true);
            StartCoroutine(PathReachCheck());
        }

        private IEnumerator PathReachCheck()
        {
            IsRun = true;
            
            while (Vector3.Distance(transform.position, _currentWaypoint.BattlePoint.position) > DISTANCE_TO_POINT)
            {
                yield return null;
            }

            StartCoroutine(RotateToPoint());
            _player.PlayerAnimationController.SetRunAnimation(false);
            _currentWaypoint.OnWaypointCleared += MoveToWaypoint;
            IsRun = false;

            if (_currentWaypoint.IsFinish)
            {
                EventStreams.UserInterface.Publish(new RestartGameEvent());
            }
        }

        private IEnumerator RotateToPoint()
        {
            var currentTime = 0f;
            var startRotation = transform.rotation;
            var finalRotation = Quaternion.LookRotation(_currentWaypoint.BattlePoint.forward);
                    
            while (currentTime < _timeRotaion)
            {
                transform.rotation = Quaternion.Slerp(startRotation, finalRotation, currentTime / _timeRotaion);
                currentTime += Time.deltaTime;
                yield return null;
            }

            transform.rotation = finalRotation;
        }

        private void MoveToWaypoint(StartGameEvent startGameEvent)
        {
            _currentWaypoint = _startWaypoint;
            MoveToWaypoint();
        }

        private void MoveToWaypoint(Waypoint waypoint)
        {
            _currentWaypoint.OnWaypointCleared -= MoveToWaypoint;
            
            _currentWaypoint = waypoint;
            MoveToWaypoint();
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}