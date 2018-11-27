// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(LoadScene))]
public class LoadSceneCustomEditor : CustomActionEditor
{
	LoadScene _target ;

	public override bool OnGUI()
	{
		_target = (LoadScene)target;

		EditField ("sceneReference");

		if (_target.sceneReference == GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex) {
			EditField ("sceneAtIndex");
		} else {
			EditField ("sceneByName");
		}

		EditField ("asynch");

		EditField ("loadSceneMode");

		EditField("found");
		EditField("foundEvent");
		EditField("notFoundEvent");

		return GUI.changed;
	}
}

#endif