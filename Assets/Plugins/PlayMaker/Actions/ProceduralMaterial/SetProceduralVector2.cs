// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Substance")]
	[Tooltip("Set a named Vector2 property in a Substance material. NOTE: Use Rebuild Textures after setting Substance properties.")]
#if UNITY_2017_3
#pragma warning disable 0618
	[Obsolete("Built-in support for Substance Designer materials has been deprecated and will be removed in Unity 2018.1. " +
	          "To continue using Substance Designer materials in Unity 2018.1, you will need to install a suitable " +
	          "third-party external importer from the Asset Store.")]
#endif
    public class SetProceduralVector2 : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The Substance Material.")]
		public FsmMaterial substanceMaterial;

		[RequiredField]
        [Tooltip("The named vector property in the material.")]
		public FsmString vector2Property;

		[RequiredField]
        [Tooltip("The Vector3 value to set the property to.\nNOTE: Use Set Procedural Vector2 for Vector3 values.")]
		public FsmVector2 vector2Value;

		[Tooltip("NOTE: Updating procedural materials every frame can be very slow!")]
		public bool everyFrame;

		public override void Reset()
		{
			substanceMaterial = null;
		    vector2Property = null;
		    vector2Value = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetProceduralVector();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetProceduralVector();
		}

	    private void DoSetProceduralVector()
        {
#if !(UNITY_2018_1_OR_NEWER || UNITY_IPHONE || UNITY_IOS || UNITY_ANDROID || UNITY_NACL || UNITY_FLASH || UNITY_PS3 || UNITY_PS4 || UNITY_XBOXONE || UNITY_BLACKBERRY || UNITY_METRO || UNITY_WP8 || UNITY_WIIU || UNITY_PSM || UNITY_WEBGL || UNITY_SWITCH)

            var substance = substanceMaterial.Value as ProceduralMaterial;
			if (substance == null)
			{
				LogError("The Material is not a Substance Material!");
				return;
			}

			substance.SetProceduralVector(vector2Property.Value, vector2Value.Value);
#endif
        }
	}
}