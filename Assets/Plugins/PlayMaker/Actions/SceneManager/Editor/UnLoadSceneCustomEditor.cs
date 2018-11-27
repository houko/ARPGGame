// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#pragma warning disable 618

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(UnloadScene))]
public class UnloadSceneCustomEditor : CustomActionEditor
{
	UnloadScene _target ;

	public override bool OnGUI()
	{
		_target = (UnloadScene)target;

		EditField ("sceneReference");

		switch (_target.sceneReference) {
		case UnloadScene.SceneReferenceOptions.ActiveScene:
			break;
		case UnloadScene.SceneReferenceOptions.SceneAtIndex:
			EditField ("sceneAtIndex");
			break;
		case UnloadScene.SceneReferenceOptions.SceneAtBuildIndex:
			EditField ("sceneAtBuildIndex");
			break;
		case UnloadScene.SceneReferenceOptions.SceneByName:
			EditField ("sceneByName");
			break;
		case UnloadScene.SceneReferenceOptions.SceneByPath:
			EditField ("sceneByPath");
			break;
		case UnloadScene.SceneReferenceOptions.SceneByGameObject:
			EditField ("sceneByGameObject");
			break;
		}


		EditField("unloaded");
		EditField("unloadedEvent");
		EditField("failureEvent");

		return GUI.changed;
	}
}

#endif