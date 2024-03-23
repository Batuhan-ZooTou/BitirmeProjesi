using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Helper
{
    public class BillBoardObject : MonoBehaviour
    {
        public Transform cameraTransform;
        private void OnEnable()
        {
            cameraTransform = Camera.main.transform;
        }
        private void Update()
        {
            transform.LookAt(cameraTransform.position, Vector3.up);
        }
    }
}