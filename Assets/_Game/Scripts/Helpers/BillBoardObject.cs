using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Extensions;

namespace _Game.Scripts.Helper
{
    public class BillBoardObject : MonoBehaviour
    {
        [SerializeField] private BillboardType billboardType;
        [SerializeField] private Vector3Bool lockState;
        [SerializeField] private bool doScale;
        private Vector3 originalRotation;
        private Transform lookAtTransform;
        private float originalScale;
        private float maxDistance=15f;

        public enum BillboardType { LookAtCamera, CameraForward };

        private void Awake()
        {
            originalRotation = transform.rotation.eulerAngles;
            originalScale = transform.localScale.x;
            lookAtTransform=Camera.main.transform;
        }

        // Use Late update so everything should have finished moving.
        void LateUpdate()
        {
            // There are two ways people billboard things.
            switch (billboardType)
            {
                case BillboardType.LookAtCamera:
                    transform.LookAt(lookAtTransform.position, Vector3.up);
                    break;
                case BillboardType.CameraForward:
                    transform.forward = lookAtTransform.forward;
                    break;
            }
            // Modify the scale 0 to originalscale distance to look target
            if (doScale)
            {
                float distance = Mathf.Clamp(Mathf.Abs(Vector3.Distance(transform.position, lookAtTransform.position)), 0,maxDistance);
                distance=distance.Remap(2.5f, maxDistance, 0.5f, originalScale);
                transform.localScale = Vector3.one*distance;
            }
            // Modify the rotation in Euler space to lock certain dimensions.
            Vector3 rotation = transform.rotation.eulerAngles;
            if (lockState.x) { rotation.x = originalRotation.x; }
            if (lockState.y) { rotation.y = originalRotation.y; }
            if (lockState.z) { rotation.z = originalRotation.z; }
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}