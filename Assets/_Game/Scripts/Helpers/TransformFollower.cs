using Sirenix.OdinInspector;
using System;
using UnityEngine;
using _Game.Scripts.Extensions;

namespace _Game.Scripts.Helper
{
    public class TransformFollower : MonoBehaviour
    {
        [SerializeField] private Transform followTarget;
        [SerializeField] private Vector3Bool position;
        [SerializeField] private float smoothPositionSpeed;
        [Tooltip("Ignores Y")][SerializeField] private bool rotation;
        [SerializeField] private float smoothRotationSpeed;
        private void Update()
        {
            if (position.x)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position.WithX(followTarget.position.x), smoothPositionSpeed*Time.deltaTime);
            }
            if (position.y)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position.WithY(followTarget.position.y), smoothPositionSpeed * Time.deltaTime);
            }
            if (position.z)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position.WithZ(followTarget.position.z), smoothPositionSpeed * Time.deltaTime);
            }
            if (rotation)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0.0f, followTarget.rotation.y, 0.0f), smoothRotationSpeed * Time.deltaTime);
            }
        }
    }
}