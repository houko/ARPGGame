// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get a scene isLoaded flag.")]
	public class GetSceneIsLoaded : GetSceneActionBase
	{
		[ActionSection("Result")]

		[Tooltip("true if the scene is loaded.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isLoaded;

		[Tooltip("Event sent if the scene is loaded.")]
		public FsmEvent isLoadedEvent;

		[Tooltip("Event sent if the scene is not loaded.")]
		public FsmEvent isNotLoadedEvent;

		[Tooltip("Repeat every Frame")]
		public bool everyFrame;
	
		public override void Reset()
		{
			base.Reset ();

			isLoaded = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			base.OnEnter ();
			DoGetSceneIsLoaded();

			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoGetSceneIsLoaded();
		}

		void DoGetSceneIsLoaded()
		{
			if (!_sceneFound) {
				return;
			}
			
			if (!isLoaded.IsNone) {
				isLoaded.Value = _scene.isLoaded;
			}

			Fsm.Event(sceneFoundEvent);
		}
	}
}

#endif