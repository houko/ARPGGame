// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_4_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Send an event when a scene was loaded. Use the Safe version when you want to access content of the loaded scene. Use GetSceneloadedEventData to find out about the loaded Scene and load mode")]
	public class SendSceneLoadedEvent : FsmStateAction
	{

		[Tooltip("The event to send when a scene was loaded")]
		public FsmEvent sceneLoaded;

		[Tooltip("The event to send when a scene was loaded, with a one frame delay to make sure the scene content was indeed initialized fully")]
		public FsmEvent sceneLoadedSafe;

		public static Scene lastLoadedScene;
		public static LoadSceneMode lastLoadedMode;

		int _loaded = -1;

		public override void Reset()
		{
			sceneLoaded = null;
		}

		public override void OnEnter()
		{
			_loaded = -1;
			SceneManager.sceneLoaded += SceneManager_sceneLoaded;
		}

		void SceneManager_sceneLoaded (Scene scene, LoadSceneMode mode)
		{
			lastLoadedScene = scene;
			lastLoadedMode = mode;
			Fsm.Event (sceneLoaded);

			_loaded = Time.frameCount;

			if (sceneLoadedSafe == null) {
				Finish ();
			}
		}
		public override void OnUpdate()
		{

			if (_loaded>-1 && Time.frameCount>_loaded) {
				_loaded = -1;
				Fsm.Event (sceneLoadedSafe);
				Finish ();
			}

		}


		public override void OnExit()
		{
			SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
		}

		public override string ErrorCheck()
		{
			if (sceneLoaded == null && sceneLoadedSafe == null) {
				return "At least one event setup is required";
			}

			return string.Empty;
		}

	}
}

#endif