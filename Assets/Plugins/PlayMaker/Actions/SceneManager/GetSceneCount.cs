// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get the total number of currently loaded scenes.")]
	public class GetSceneCount : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The number of currently loaded scenes.")]
		public FsmInt sceneCount;

		[Tooltip("Repeat every Frame")]
		public bool everyFrame;

		public override void Reset()
		{
			sceneCount = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetSceneCount ();

			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate()
		{
			DoGetSceneCount ();
		}

		void DoGetSceneCount()
		{
			sceneCount.Value =	SceneManager.sceneCount;
		}
	}
}

#endif