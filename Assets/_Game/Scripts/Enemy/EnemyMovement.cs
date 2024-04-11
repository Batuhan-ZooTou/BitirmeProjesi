using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using _Game.Scripts.Extensions;
namespace _Game.Scripts.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Enemy enemy;
        [SerializeField] private NavMeshPath path;
        [SerializeField] private bool showGizmos;
        [SerializeField] private float defaultMoveSpeed;
        [SerializeField][Range(0f, 3f)] private float rotationSmoothing;
        [SerializeField][Range(0.1f,1f)] private float slowRatioOnTurn;
        private float _rotationVelocity;
        private void Start()
        {
            path = new NavMeshPath();
        }
        public void MoveTowardsTarget(Vector3 position)
        {
            if (enemy.navMeshAgent.CalculatePath(position, path))
            {
                float dot = Vector3.Dot((enemy.currentDestination.position - transform.position).normalized, transform.forward.normalized);
                Vector3 dir = (path.corners[1] - transform.position).normalized;
                float rotation = Vector3.SignedAngle(dir, transform.forward, Vector3.up);
                rotation = rotation.Remap(-180, 180, -1, 1);
                enemy.animationController.SetMovement(-rotation, dot, false, 1);
                dot = dot.Remap(-1, 1, 1 * slowRatioOnTurn, 1);
                float currentSpeed = defaultMoveSpeed/100 * dot;
                enemy.navMeshAgent.speed = currentSpeed;
                enemy.navMeshAgent.Move(dir *currentSpeed);
                RotateTowardsTarget(dir);
            }
        }
        public void RotateTowardsTarget(Vector3 direction)
        {
            Quaternion lookRotation =Quaternion.LookRotation(direction, Vector3.up);
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, lookRotation.eulerAngles.y, ref _rotationVelocity, rotationSmoothing);
            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
        public void Stop()
        {
        }
        private void OnDrawGizmos()
        {
            if (!showGizmos)
                return;
            if (enemy.currentDestination==null)
                return;
            //Draw path
            if (NavMesh.CalculatePath(transform.position, enemy.currentDestination.position, enemy.navMeshAgent.areaMask, path))
            {
                for (int i = 0; i < path.corners.Length - 1; i++)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(path.corners[i], path.corners[i + 1]);
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(path.corners[i + 1], 0.15f);
                }
            }
        }
    }
}