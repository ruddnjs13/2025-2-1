using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace _01.Code.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        public UnityEvent OnArriveEvent;
        
        private NavMeshAgent _navAgent;
        
        [SerializeField] private List<Transform> wayPoints;
        [SerializeField] private EnemyRenderer enemyRenderer;
        private int _currentIndex = 0;
        
        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
            _navAgent.stoppingDistance = 0f;
            _navAgent.updateRotation = false;
            _navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        }

        public void SetWayPoints(List<Transform> wayPoints)
        {
            this.wayPoints = wayPoints;
        }

        public void EnableEnemy()
        {
            _currentIndex = 0;
            _navAgent.SetDestination(wayPoints[_currentIndex++].position);
        }


        public void SetSpeed(float speed) => _navAgent.speed = speed;
        public void SetStop(bool isStop) => _navAgent.isStopped = isStop;
        public float GetDistance() => _navAgent.remainingDistance;
        public int GetWaypointIdx() => _currentIndex;

        private void Update()
        {
            if (_navAgent.remainingDistance <= _navAgent.stoppingDistance)
            {
                if (_currentIndex >= wayPoints.Count)
                {
                    OnArriveEvent?.Invoke();
                }
                else
                {
                    _navAgent.SetDestination(wayPoints[_currentIndex++].position);
                    enemyRenderer.FlipByCamera(_navAgent.destination - transform.position);
                }
            }
        }
    }
}