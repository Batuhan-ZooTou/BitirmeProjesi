using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace _Game.Scripts.Helper
{
    [Serializable]
    public class QuadraticCurve :MonoBehaviour
    {
        [HideInInspector] public List<Vector3> followPoints = new List<Vector3>();
        [SerializeField] private bool showGizmos;
        [SerializeField] private float resolution;
        public void GetTrajectory(Vector3 startPosition, Vector3 heightPosition, Vector3 endPosition)
        {
            followPoints.Add(startPosition);
            for (int i = 1; i <= resolution; i++)
            {
                followPoints.Add(Evaluate(startPosition, heightPosition, endPosition, i / resolution));
            }
            DrawProjectory();
        }
        public Vector3 Evaluate(Vector3 startPosition, Vector3 heightPosition, Vector3 endPosition,float t)
        {
            Vector3 ac = Vector3.Lerp(startPosition, heightPosition, t);
            Vector3 cb = Vector3.Lerp(heightPosition, endPosition, t);
            return Vector3.Lerp(ac, cb, t);
        }
        public Vector3 Evaluate(Vector3 startPosition, Vector3 endPosition, float height, float t)
        {
            Vector3 controlPoint = startPosition+(endPosition-startPosition)/2 + (Vector3.up * height);
            Vector3 ac = Vector3.Lerp(startPosition, controlPoint, t);
            Vector3 cb = Vector3.Lerp(controlPoint, endPosition, t);
            return Vector3.Lerp(ac, cb, t);
        }
        private void DrawProjectory()
        {
            if (!showGizmos)
                return;
            foreach (var point in followPoints)
            {
                Gizmos.DrawWireSphere(point, 0.1f);
            }
        }
    }
}