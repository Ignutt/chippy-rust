using System;
using Common;
using UnityEngine;
using UnityEngine.Windows.WebCam;

namespace Gameplay.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Spawn properties")]
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private Transform spawnPoint;

        [Header("Attack properties")] 
        [SerializeField] private float speedRotation = 60f;
        [SerializeField] private Transform center;
        [SerializeField] private Transform directionPoint;

        private void Start()
        {
            InputManager.Instance.OnAttack += () =>
            {
                Attack();
            };

            InputManager.Instance.OnMouseX += value =>
            {
                if (value == 0) return;
                
                center.Rotate(Vector3.back * speedRotation * Time.deltaTime * value);
            };
        }

        private void Attack()
        {
            Projectile projectile = Instantiate(projectilePrefab, spawnPoint.position, center.rotation);

            projectile.Direction = directionPoint.position;
        }
    }
}
