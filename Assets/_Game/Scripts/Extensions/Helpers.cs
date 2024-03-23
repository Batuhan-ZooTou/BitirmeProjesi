using Sirenix.OdinInspector;
using System;

namespace _Game.Scripts.Extensions
{
    public static class Helpers
    {
        public static bool IsNull(Type t)
        {
            return ReferenceEquals(t, null);
        }
        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
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