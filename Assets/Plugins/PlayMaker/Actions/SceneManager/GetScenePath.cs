// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get a scene path.")]
	public class GetScenePath : GetSceneActionBase
	{
		[ActionSection("Result")]

		[Tooltip("The scene path")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString path;

	
		public override void Reset()
		{
			base.Reset ();

			path = null;
		}

		public override void OnEnter()
		{
			base.OnEnter ();

			DoGetScenePath();

			Finish();
		}

		void DoGetScenePath()
		{
			if (!_sceneFound) {
				return;
			}
			
			if (!path.IsNone) {
				path.Value = _scene.path;
			}

			Fsm.Event(sceneFoundEvent);
		}
	}
}

#endif