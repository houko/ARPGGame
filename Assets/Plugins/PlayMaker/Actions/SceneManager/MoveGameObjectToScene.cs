// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Move a GameObject from its current scene to a new scene. It is required that the GameObject is at the root of its current scene.")]
	public class MoveGameObjectToScene : GetSceneActionBase
	{
		[RequiredField]
		[Tooltip("The Root GameObject to move to the referenced scene")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Only root GameObject can be moved, set to true to get the root of the gameobject if necessary, else watch for failure events.")]
		public FsmBool findRootIfNecessary;

		[ActionSection("Result")]
		[Tooltip("True if the merge succeeded")]
		[UIHint(UIHint.Variable)]
		public FsmBool success;

		[Tooltip("Event sent if merge succeeded")]
		public FsmEvent successEvent;

		[Tooltip("Event sent if merge failed. Check log for information")]
		public FsmEvent failureEvent;

		GameObject _go;

		public override void Reset()
		{
			base.Reset ();
			gameObject = null;
			findRootIfNecessary = null;
			success = null;
			successEvent = null;
			failureEvent = null;
		}

		public override void OnEnter()
		{
			base.OnEnter ();

			if (_sceneFound) {

				_go = Fsm.GetOwnerDefaultTarget (gameObject);

				if (findRootIfNecessary.Value) {
					_go = _go.transform.root.gameObject;
				}

				if (_go.transform.parent == null) {
					SceneManager.MoveGameObjectToScene(_go, _scene);
					success.Value = true;
					Fsm.Event (successEvent);

				}else{
					
					LogError("GameObject must be a root ");
					success.Value = false;
					Fsm.Event(failureEvent);
				}

				Fsm.Event(sceneFoundEvent);

				_go = null;
			}

			Finish();
		}
	}
}

#endif