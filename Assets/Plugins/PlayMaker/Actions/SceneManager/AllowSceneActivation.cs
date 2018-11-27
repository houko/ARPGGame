// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Allow scenes to be activated. Use this after LoadSceneAsynch where you did not activated the scene upon loading")]
	public class AllowSceneActivation : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The name of the new scene. It cannot be empty or null, or same as the name of the existing scenes.")]
		public FsmInt aSynchOperationHashCode;

		[Tooltip("Allow the scene to be activated")]
		public FsmBool allowSceneActivation;

		[ActionSection("Result")]

		[Tooltip("The loading's progress.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat progress;

		[Tooltip("True when loading is done")]
		[UIHint(UIHint.Variable)]
		public FsmBool isDone;

		[Tooltip("Event sent when scene loading is done")]
		public FsmEvent doneEvent;

		[Tooltip("Event sent when action could not be performed. Check Log for more information")]
		public FsmEvent failureEvent;

		public override void Reset()
		{
			aSynchOperationHashCode = null;
			allowSceneActivation = true;

			progress = null;
			isDone = null;
			doneEvent = null;
			failureEvent = null;
		}

		public override void OnEnter()
		{
			DoAllowSceneActivation ();
		}

		public override void OnUpdate()
		{
			if (!progress.IsNone)
				progress.Value = LoadSceneAsynch.aSyncOperationLUT [aSynchOperationHashCode.Value].progress;
			
			if (!isDone.IsNone) {
				isDone.Value = LoadSceneAsynch.aSyncOperationLUT [aSynchOperationHashCode.Value].isDone;
			}

			if (LoadSceneAsynch.aSyncOperationLUT [aSynchOperationHashCode.Value].isDone) {
				LoadSceneAsynch.aSyncOperationLUT.Remove (aSynchOperationHashCode.Value);
				Fsm.Event (doneEvent);
				Finish ();
				return;
			}
		}


		void DoAllowSceneActivation()
		{
			if (aSynchOperationHashCode.IsNone ||
				allowSceneActivation.IsNone ||
				LoadSceneAsynch.aSyncOperationLUT==null ||
				!LoadSceneAsynch.aSyncOperationLUT.ContainsKey(aSynchOperationHashCode.Value)
			) {
				Fsm.Event(failureEvent);
				Finish();
				return;
			}
				
			LoadSceneAsynch.aSyncOperationLUT [aSynchOperationHashCode.Value].allowSceneActivation = allowSceneActivation.Value;
		}
	}
}

#endif