using System;
using Common;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [Header("Movement properties")]
        [SerializeField] private RangeFloat speedRange;
        [SerializeField] private Transform[] targetsPoints;

        private Rigidbody2D _rigidbody;
        private int _currentTargetIndex;
        private float _speedMovement;

        private Vector2 CurrentTarget => targetsPoints[_currentTargetIndex].position;

        public event Action OnChangeTarget;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _speedMovement = speedRange.Random();

            OnChangeTarget += () =>
            {
                _speedMovement = speedRange.Random();
            };
        }

        private void FixedUpdate()
        {
            MoveToTarget();
            CheckTarget();
        }

        private void MoveToTarget()
        {
            Vector3 newPosition = Vector3.MoveTowards(
                _rigidbody.position,
                CurrentTarget,
                _speedMovement * Time.deltaTime);
            
            _rigidbody.MovePosition(newPosition);
        }

        private void CheckTarget()
        {
            if (_rigidbody.position == CurrentTarget)
            {
                ChangeTarget();
            }
        }

        private void ChangeTarget()
        {
            _currentTargetIndex++;
            if (_currentTargetIndex >= targetsPoints.Length)
            {
                _currentTargetIndex = 0;
            }
            
            OnChangeTarget?.Invoke();
        }
    }
}
