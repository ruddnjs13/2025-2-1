using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace _01.Code.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
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

        private void OnEnable()
        {
        }

        private void Start()
        {
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
                    //나중에 피 까이기
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