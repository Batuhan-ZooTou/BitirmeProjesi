using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace _Game.Scripts.Extensions
{
    public static class Helpers
    {
        /// <summary>
        /// 5x faster than x == null
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsNull(this System.Object reference)
        {
            return ReferenceEquals(reference, null);
        }
        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
        public static float ClampedRemap(this float value, float from1, float to1, float from2, float to2)
        {
            return Mathf.Clamp((value - from1) / (to1 - from1) * (to2 - from2) + from2, from2, to2);
        }
        public static float ClampAngle(this float value,float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
    [Serializable]
    [InlineProperty(LabelWidth = 13)]
    public struct Vector3Bool
    {
        [HorizontalGroup]
        public bool x;

        [HorizontalGroup]
        public bool y;

        [HorizontalGroup]
        public bool z;
    }
}