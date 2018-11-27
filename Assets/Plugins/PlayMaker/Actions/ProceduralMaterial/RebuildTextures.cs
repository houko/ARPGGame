// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.


using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Substance")]
	[Tooltip("Rebuilds all dirty textures. By default the rebuild is spread over multiple frames so it won't halt the game. Check Immediately to rebuild all textures in a single frame.")]
#if UNITY_2017_3
#pragma warning disable 0618
	[Obsolete("Built-in support for Substance Designer materials has been deprecated and will be removed in Unity 2018.1. " +
	          "To continue using Substance Designer materials in Unity 2018.1, you will need to install a suitable " +
	          "third-party external importer from the Asset Store.")]
#endif
    public class RebuildTextures : FsmStateAction
	{
		[RequiredField]
		public FsmMaterial substanceMaterial;
		
		[RequiredField]
		public FsmBool immediately;
		
		public bool everyFrame;

		public override void Reset()
		{
			substanceMaterial = null;
			immediately = false;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoRebuildTextures();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoRebuildTextures();
		}

		void DoRebuildTextures()
        {
#if !(UNITY_2018_1_OR_NEWER || UNITY_IPHONE || UNITY_IOS || UNITY_ANDROID || UNITY_NACL || UNITY_FLASH || UNITY_PS3 || UNITY_PS4 || UNITY_XBOXONE || UNITY_BLACKBERRY || UNITY_METRO || UNITY_WP8 || UNITY_WIIU || UNITY_PSM || UNITY_WEBGL || UNITY_SWITCH)

            var substance = substanceMaterial.Value as ProceduralMaterial;

			if (substance == null)
			{
				LogError("Not a substance material!");
				return;
			}

			if (!immediately.Value)
			{
				substance.RebuildTextures();
			}
			else
			{
				substance.RebuildTexturesImmediately();
			}

#endif
        }
	}
}
