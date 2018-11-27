// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get a scene name.")]
	public class GetSceneName : GetSceneActionBase
	{
		[ActionSection("Result")]

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The scene name")]
		public FsmString name;

	
		public override void Reset()
		{
			base.Reset ();

			name = null;
		}

		public override void OnEnter()
		{
			base.OnEnter ();

			DoGetSceneName();

			Finish();
		}

		void DoGetSceneName()
		{
			if (!_sceneFound) {
				return;
			}
			
			if (!name.IsNone) {
				name.Value = _scene.name;
			}

			Fsm.Event(sceneFoundEvent);
		}
	}
}

#endif