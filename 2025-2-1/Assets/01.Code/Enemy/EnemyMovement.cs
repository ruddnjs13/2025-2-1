using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace _01.Code.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private NavMeshAgent _navAgent;
        
        [SerializeField] private List<Transform> wayPoints;
        private int _currentIndex = 0;
        
        
        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
            _navAgent.stoppingDistance = 0f;
        }

        private void OnEnable()
        {
            _navAgent.SetDestination(wayPoints[_currentIndex++].position);
        }

        public void SetSpeed(float speed) => _navAgent.speed = speed;
        public void SetStop(bool isStop) => _navAgent.isStopped = isStop;

        private void Update()
        {
            if (_navAgent.remainingDistance <= _navAgent.stoppingDistance)
            {
                _navAgent.SetDestination(wayPoints[_currentIndex++].position);
            }
        }
    }
}