// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(LoadSceneAsynch))]
public class LoadSceneAsynchCustomEditor : CustomActionEditor
{
	LoadSceneAsynch _target ;

	public override bool OnGUI()
	{
		_target = (LoadSceneAsynch)target;

		EditField ("sceneReference");

		if (_target.sceneReference == GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex) {
			EditField ("sceneAtIndex");
		} else {
			EditField ("sceneByName");
		}
			

		EditField ("loadSceneMode");

		EditField("allowSceneActivation");
		EditField("operationPriority");



		EditField ("aSyncOperationHashCode");
		EditField("progress");
		EditField("pendingActivation");
		EditField("isDone");

		EditField("doneEvent");
		EditField("pendingActivationEvent");
		EditField("sceneNotFoundEvent");

		return GUI.changed;
	}
}

#endif