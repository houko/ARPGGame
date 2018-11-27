// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get a scene RootCount, the number of root transforms of this scene.")]
	public class GetSceneRootCount : GetSceneActionBase
	{
		[ActionSection("Result")]

		[Tooltip("The scene RootCount")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt rootCount;

		[Tooltip("Repeat every frame")]
		public bool everyFrame;
	
		public override void Reset()
		{
			base.Reset ();

			rootCount = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			base.OnEnter ();

			DoGetSceneRootCount();

			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoGetSceneRootCount();
		}

		void DoGetSceneRootCount()
		{
			if (!_sceneFound) {
				return;
			}
			
			if (!rootCount.IsNone) {
				rootCount.Value = _scene.rootCount;
			}

			Fsm.Event(sceneFoundEvent);
		}
	}
}

#endif