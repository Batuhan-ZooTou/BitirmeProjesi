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