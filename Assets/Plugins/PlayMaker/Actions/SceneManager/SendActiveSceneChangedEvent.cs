// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_4_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Send an event when the active scene has changed.")]
	public class SendActiveSceneChangedEvent : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The event to send when an active scene changed")]
		public FsmEvent activeSceneChanged;

		public static Scene lastPreviousActiveScene;
		public static Scene lastNewActiveScene;

		public override void Reset()
		{
			activeSceneChanged = null;
		}

		public override void OnEnter()
		{
			SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;

			Finish();
		}

		void SceneManager_activeSceneChanged (Scene previousActiveScene , Scene activeScene)
		{

			lastNewActiveScene = activeScene;
			lastPreviousActiveScene = previousActiveScene;

			Fsm.Event (activeSceneChanged);

			Finish ();
		}

		public override void OnExit()
		{
			SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
		}
	}
}

#endif