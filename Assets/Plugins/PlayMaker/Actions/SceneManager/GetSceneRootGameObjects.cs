// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if !UNITY_5_3_0 && !UNITY_5_3_1 && (  UNITY_5_3 || UNITY_5_3_OR_NEWER )

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get a scene Root GameObjects.")]
	public class GetSceneRootGameObjects : GetSceneActionBase
	{
		[ActionSection("Result")]

		[Tooltip("The scene Root GameObjects")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject)]
		public FsmArray rootGameObjects;

		[Tooltip("Repeat every Frame")]
		public bool everyFrame;

		public override void Reset()
		{
			base.Reset ();

			rootGameObjects = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			base.OnEnter ();

			DoGetSceneRootGameObjects();

			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoGetSceneRootGameObjects();
		}

		void DoGetSceneRootGameObjects()
		{
			if (!_sceneFound) {
				return;
			}
			
			if (!rootGameObjects.IsNone) {
				rootGameObjects.Values = _scene.GetRootGameObjects();
			}

			Fsm.Event(sceneFoundEvent);
		}
	}
}

#endif