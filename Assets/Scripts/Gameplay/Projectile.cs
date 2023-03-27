using System;
using UnityEngine;

namespace Gameplay
{
    public class Projectile : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float speedMovement = 7f;
        [SerializeField] private Transform directionPoint;

        private Rigidbody2D _rigidbody;

        public Vector3 Direction
        {
            get => directionPoint.position;
            set => directionPoint.position = value;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            MoveToTarget();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
        }

        private void MoveToTarget()
        {
            Vector3 newPosition = Vector3.MoveTowards(
                _rigidbody.position,
                directionPoint.position,
                speedMovement * Time.deltaTime);
            
            _rigidbody.MovePosition(newPosition);
        }
        
    }
}