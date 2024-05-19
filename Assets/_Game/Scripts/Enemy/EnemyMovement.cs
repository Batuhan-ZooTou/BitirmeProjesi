using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using _Game.Scripts.Extensions;
using Sirenix.OdinInspector;

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
        [SerializeField] private bool canMove;
        [SerializeField] private bool canLook;
        private float _rotationVelocity;
        private void Start()
        {
            path = new NavMeshPath();
        }
        public void MoveTowardsTarget(Vector3 movePosition,Vector3 lookPosition)
        {
            if (enemy.navMeshAgent.CalculatePath(movePosition, path))
            {
                //Dot product to destination
                float dot = Vector3.Dot((movePosition - transform.position).normalized, transform.forward.normalized);
                //Move Direction to next corner
                Vector3 moveDirection = (path.corners[1] - transform.position).normalized;
                //Look direction changes if he has look target
                Vector3 lookDirection;
                if (movePosition!= lookPosition)
                {
                   lookDirection = (lookPosition - transform.position).normalized;
                }
                else
                {
                    lookDirection = moveDirection;
                }
                //rotate towards target
                if (canLook)
                {
                    //Rotate
                    RotateTowardsTarget(lookDirection);
                }
                Debug.DrawLine(transform.position, transform.position + moveDirection, Color.red);
                Debug.DrawLine(transform.position, transform.position + lookDirection, Color.green);
                
                //move along direction
                if (canMove)
                {
                    //which side it should turn (Kinda inverse of dot product it calculates 0 to 1 insted of 1-0)
                    float rotation = Vector3.SignedAngle(lookDirection, moveDirection, Vector3.up);
                    if (Mathf.Abs(rotation) > 90)
                    {
                        float diffrence = Mathf.Abs(rotation) - 90;
                        if (rotation < 0)
                        {
                            rotation = -90 + diffrence;
                        }
                        else
                        {
                            rotation = 90 - diffrence;
                        }
                    }
                    rotation = rotation.Remap(-90, 90, -1, 1);
                    //Set move animation
                    enemy.animationController.SetMovement(rotation, dot, false, 1);
                    //Calculate current move speed
                    dot = dot.Remap(-1, 1, 1 * slowRatioOnTurn, 1);
                    float currentSpeed = defaultMoveSpeed / 100 * dot;
                    enemy.navMeshAgent.speed = currentSpeed;
                    //Move
                    enemy.navMeshAgent.Move(moveDirection * currentSpeed);
                }
                
            }
        }
        public void ChangeTarget(Transform newDestination = null, Transform newLookTarget = null)
        {
            if (!newDestination.IsNull())
            {
                enemy.currentDestination = newDestination;
            }
            if (!newLookTarget.IsNull())
            {
                enemy.currentLookTarget = newLookTarget;
            }
        }
        public void RotateTowardsTarget(Vector3 direction)
        {
            Quaternion lookRotation =Quaternion.LookRotation(direction, Vector3.up);
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, lookRotation.eulerAngles.y, ref _rotationVelocity, rotationSmoothing);
            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
        public void StopMovement()
        {
            canMove = false;
        }
        public void ContinueMovement()
        {
            canMove = true;
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