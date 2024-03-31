// Toony Colors Pro+Mobile 2
// (c) 2014-2022 Jean Moreno

using UnityEngine;
using UnityEngine.Rendering;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Use this script to generate the Reflection Render Texture when using the "Planar Reflection" mode from the Shader Generator

// Usage:
// - generate a water shader with "Planar Reflection"
// - assign this shader to a planar mesh's material
// - add this script on the same GameObject

// Based on: http://wiki.unity3d.com/index.php/MirrorReflection4

namespace ToonyColorsPro
{
	namespace Runtime
	{
		[ExecuteInEditMode]
		public class TCP2_PlanarReflection : MonoBehaviour
		{
		}
	}
}