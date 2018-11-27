// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_4_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get the last activateChanged Scene Event data when event was sent from the action 'SendSceneActiveChangedEvent")]
	public class GetSceneActivateChangedEventData : FsmStateAction
	{

		[ActionSection("New Active Scene")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The new active scene name")]
		public FsmString newName;

		[Tooltip("The new active scene path")]
		[UIHint(UIHint.Variable)]
		public FsmString newPath;

		[Tooltip("true if the new active scene is valid.")]
		[UIHint(UIHint.Variable)]
		public FsmBool newIsValid;

		[Tooltip("The new active scene Build Index")]
		[UIHint(UIHint.Variable)]
		public FsmInt newBuildIndex;

		[Tooltip("true if the new active scene is loaded.")]
		[UIHint(UIHint.Variable)]
		public FsmBool newIsLoaded;

		[UIHint(UIHint.Variable)]
		[Tooltip("true if the new active scene is modified.")]
		public FsmBool newIsDirty;

		[Tooltip("The new active scene RootCount")]
		[UIHint(UIHint.Variable)]
		public FsmInt newRootCount;

		[Tooltip("The new active scene Root GameObjects")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject)]
		public FsmArray newRootGameObjects;

		[ActionSection("Previous Active Scene")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The previous active scene name")]
		public FsmString previousName;

		[Tooltip("The previous active scene path")]
		[UIHint(UIHint.Variable)]
		public FsmString previousPath;

		[Tooltip("true if the previous active scene is valid.")]
		[UIHint(UIHint.Variable)]
		public FsmBool previousIsValid;

		[Tooltip("The previous active scene Build Index")]
		[UIHint(UIHint.Variable)]
		public FsmInt previousBuildIndex;

		[Tooltip("true if the previous active scene is loaded.")]
		[UIHint(UIHint.Variable)]
		public FsmBool previousIsLoaded;

		[UIHint(UIHint.Variable)]
		[Tooltip("true if the previous active scene is modified.")]
		public FsmBool previousIsDirty;

		[Tooltip("The previous active scene RootCount")]
		[UIHint(UIHint.Variable)]
		public FsmInt previousRootCount;

		[Tooltip("The previous active scene Root GameObjects")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject)]
		public FsmArray previousRootGameObjects;

		Scene _scene;
	
		public override void Reset()
		{

			newName = null;
			newPath = null;
			newIsValid = null;
			newBuildIndex = null;
			newIsLoaded = null;
			newRootCount = null;
			newRootGameObjects = null;
			newIsDirty = null;

			previousName = null;
			previousPath = null;
			previousIsValid = null;
			previousBuildIndex = null;
			previousIsLoaded = null;
			previousRootCount = null;
			previousRootGameObjects = null;
			previousIsDirty = null;
		}

		public override void OnEnter()
		{
			DoGetSceneProperties();

			Finish();
		}

		public override void OnUpdate()
		{
			DoGetSceneProperties();
		}

		void DoGetSceneProperties()
		{

			_scene = SendActiveSceneChangedEvent.lastPreviousActiveScene;

			if (!previousName.IsNone) {
				previousName.Value = _scene.name;
			}

			if (!previousBuildIndex.IsNone) {
				previousBuildIndex.Value = _scene.buildIndex;
			}

			if (!previousPath.IsNone) {
				previousPath.Value = _scene.path;
			}

			if (!previousIsValid.IsNone) {
				previousIsValid.Value = _scene.IsValid();
			}

			if (!previousIsDirty.IsNone) {
				previousIsDirty.Value = _scene.isDirty;
			}

			if (!previousIsLoaded.IsNone) {
				previousIsLoaded.Value = _scene.isLoaded;
			}

			if (!previousRootCount.IsNone) {
				previousRootCount.Value = _scene.rootCount;
			}

			if (!previousRootGameObjects.IsNone) {
				if (_scene.IsValid ()) {
					previousRootGameObjects.Values = _scene.GetRootGameObjects ();
				} else {
					previousRootGameObjects.Resize (0);
				}
			}


			_scene = SendActiveSceneChangedEvent.lastNewActiveScene;

			if (!newName.IsNone) {
				newName.Value = _scene.name;
			}

			if (!newBuildIndex.IsNone) {
				newBuildIndex.Value = _scene.buildIndex;
			}

			if (!newPath.IsNone) {
				newPath.Value = _scene.path;
			}

			if (!newIsValid.IsNone) {
				newIsValid.Value = _scene.IsValid();
			}

			if (!newIsDirty.IsNone) {
				newIsDirty.Value = _scene.isDirty;
			}

			if (!newIsLoaded.IsNone) {
				newIsLoaded.Value = _scene.isLoaded;
			}

			if (!newRootCount.IsNone) {
				newRootCount.Value = _scene.rootCount;
			}

			if (!newRootGameObjects.IsNone) {
				if (_scene.IsValid ()) {
					newRootGameObjects.Values = _scene.GetRootGameObjects ();
				} else {
					newRootGameObjects.Resize (0);
				}
			}


		}
	}
}

#endif