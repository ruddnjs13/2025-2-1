using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Code.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        public UnityEvent OnArriveEvent;
        
        private NavMeshAgent _navAgent;
        private List<Transform> _wayPoints;
        
        [SerializeField] private EnemyRenderer enemyRenderer;
        [field: SerializeField] public int CurrentIndex { get; private set; } = 0;

        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
            
            _navAgent.stoppingDistance = 0.1f;
            _navAgent.updateRotation = false;
            _navAgent.updateUpAxis = false;
            _navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        }

        public void SetWayPoints(List<Transform> wayPoints)
        {
            _wayPoints = wayPoints;
        }

        public void resetPosition()
        {
            if (_wayPoints != null && _wayPoints.Count > 0)
            {
                _navAgent.Warp(_wayPoints[0].position);
            }
        }

        public void EnableEnemy()
        {
            CurrentIndex = 0;
            SetDestinationToNextWaypoint();
        }

        public void SetSpeed(float speed) => _navAgent.speed = speed;
        public void SetStop(bool isStop) => _navAgent.isStopped = isStop;
        public float GetDistance() => _navAgent.remainingDistance;
        public int GetWaypointIdx() => CurrentIndex;

        private void Update()
        {
            if (_navAgent.pathPending) return;

            if (_navAgent.remainingDistance <= _navAgent.stoppingDistance)
            {
                if (CurrentIndex >= _wayPoints.Count)
                {
                    OnArriveEvent?.Invoke();
                }
                else
                {
                    SetDestinationToNextWaypoint();
                }
            }
        }

        private void SetDestinationToNextWaypoint()
        {
            if (_wayPoints == null || CurrentIndex >= _wayPoints.Count) return;

            Vector3 targetPos = _wayPoints[CurrentIndex++].position;
            _navAgent.SetDestination(targetPos);
            
            Vector3 direction = targetPos - transform.position;
            if (direction.sqrMagnitude > 0.01f)
            {
                enemyRenderer.FlipByCamera(direction);
            }
        }
        
        public void SetIdx(int idx)
        {
            CurrentIndex = idx;
            _navAgent.SetDestination(_wayPoints[CurrentIndex].position);

        }
    }
}