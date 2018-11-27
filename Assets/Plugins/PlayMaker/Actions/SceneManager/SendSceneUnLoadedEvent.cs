// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_4_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Send an event when a scene was unloaded.")]
	public class SendSceneUnloadedEvent : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The event to send when scene was unloaded")]
		public FsmEvent sceneUnloaded;
	
		public static Scene lastUnLoadedScene;

		public override void Reset()
		{
			sceneUnloaded = null;
		}

		public override void OnEnter()
		{
			SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;

			Finish();
		}

		void SceneManager_sceneUnloaded (Scene scene)
		{
			UnityEngine.Debug.Log(scene.name);

			lastUnLoadedScene = scene;

			Fsm.Event (sceneUnloaded);

			Finish ();
		}

		public override void OnExit()
		{
			SceneManager.sceneUnloaded -= SceneManager_sceneUnloaded;
		}
	}
}

#endif