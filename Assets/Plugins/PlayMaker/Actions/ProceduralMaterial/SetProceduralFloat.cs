// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Substance")]
	[Tooltip("Set a named float property in a Substance material. NOTE: Use Rebuild Textures after setting Substance properties.")]
#if UNITY_2017_3
#pragma warning disable 0618
	[Obsolete("Built-in support for Substance Designer materials has been deprecated and will be removed in Unity 2018.1. " +
	          "To continue using Substance Designer materials in Unity 2018.1, you will need to install a suitable " +
	          "third-party external importer from the Asset Store.")]
#endif
    public class SetProceduralFloat : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The Substance Material.")]
		public FsmMaterial substanceMaterial;

		[RequiredField]
        [Tooltip("The named float property in the material.")]
		public FsmString floatProperty;

		[RequiredField]
        [Tooltip("The value to set the property to.")]
		public FsmFloat floatValue;

		[Tooltip("NOTE: Updating procedural materials every frame can be very slow!")]
		public bool everyFrame;

		public override void Reset()
		{
			substanceMaterial = null;
			floatProperty = "";
			floatValue = 0f;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetProceduralFloat();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetProceduralFloat();
		}

	    private void DoSetProceduralFloat()
        {
#if !(UNITY_2018_1_OR_NEWER || UNITY_IPHONE || UNITY_IOS || UNITY_ANDROID || UNITY_NACL || UNITY_FLASH || UNITY_PS3 || UNITY_PS4 || UNITY_XBOXONE || UNITY_BLACKBERRY || UNITY_METRO || UNITY_WP8 || UNITY_WIIU || UNITY_PSM || UNITY_WEBGL || UNITY_SWITCH )

            var substance = substanceMaterial.Value as ProceduralMaterial;
			if (substance == null)
			{
				LogError("The Material is not a Substance Material!");
				return;
			}

			substance.SetProceduralFloat(floatProperty.Value, floatValue.Value);
#endif
        }
	}
}