// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Set the scene to be active.")]
	public class SetActiveScene : FsmStateAction
	{
		public enum SceneReferenceOptions {SceneAtBuildIndex,SceneAtIndex,SceneByName,SceneByPath,SceneByGameObject};


		[Tooltip("The reference options of the Scene")]
		public SceneReferenceOptions sceneReference;

		[Tooltip("The name of the scene to activate. The given sceneName can either be the last part of the path, without .unity extension or the full path still without the .unity extension")]
		public FsmString sceneByName;

		[Tooltip("The build index of the scene to activate.")]
		public FsmInt sceneAtBuildIndex;

		[Tooltip("The index of the scene to activate.")]
		public FsmInt sceneAtIndex;

		[Tooltip("The scene Path.")]
		public FsmString sceneByPath;

		[Tooltip("The GameObject scene to activate")]
		public FsmOwnerDefault sceneByGameObject;


		[ActionSection("Result")]

		[Tooltip("True if set active succeeded")]
		[UIHint(UIHint.Variable)]
		public FsmBool success;

		[Tooltip("Event sent if setActive succeeded ")]
		public FsmEvent successEvent;

		[Tooltip("True if SceneReference resolves to a scene")]
		[UIHint(UIHint.Variable)]
		public FsmBool sceneFound;

		[Tooltip("Event sent if scene not activated yet")]
		[UIHint(UIHint.Variable)]
		public FsmEvent sceneNotActivatedEvent;

		[Tooltip("Event sent if SceneReference do not resolve to a scene")]
		public FsmEvent sceneNotFoundEvent;

		Scene _scene;
		bool _sceneFound;
		bool _success;

		public override void Reset()
		{
			sceneReference = SceneReferenceOptions.SceneAtIndex;
			sceneByName = null;
			sceneAtBuildIndex = null;
			sceneAtIndex = null;
			sceneByPath = null;
			sceneByGameObject = null;

			success = null;
			successEvent = null;
			sceneFound = null;
			sceneNotActivatedEvent = null;
			sceneNotFoundEvent = null;
		}

		public override void OnEnter()
		{
			DoSetActivate ();

			if (!success.IsNone) {
				success.Value = _success;
			}

			if (!sceneFound.IsNone) {
				sceneFound.Value = _sceneFound;
			}

			if (_success) {
				Fsm.Event (successEvent);
			}

		}

		void DoSetActivate()
		{
			try{
				switch (sceneReference) {
				case SceneReferenceOptions.SceneAtIndex:
					_scene = SceneManager.GetSceneAt (sceneAtIndex.Value);	
					break;
				case SceneReferenceOptions.SceneByName:
					_scene = SceneManager.GetSceneByName (sceneByName.Value);
					break;
				case SceneReferenceOptions.SceneByPath:
					_scene = SceneManager.GetSceneByPath (sceneByPath.Value);
					break;
				case SceneReferenceOptions.SceneByGameObject:
					GameObject _go = Fsm.GetOwnerDefaultTarget (sceneByGameObject);
					if (_go==null)
					{
						throw new  Exception ("Null GameObject");
					}else{
						_scene =_go.scene;
					}
					break;
				}
			}catch(Exception e) {
				LogError (e.Message);
				_sceneFound = false;
				Fsm.Event(sceneNotFoundEvent);
				return;
			}

			if (_scene == new Scene()) {
				_sceneFound = false;
				Fsm.Event(sceneNotFoundEvent);
			} else {
				_success = SceneManager.SetActiveScene (_scene);
				_sceneFound = true;
			}
		}
	}
}

#endif