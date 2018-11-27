// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(SetActiveScene))]
public class SetActiveSceneCustomEditor : CustomActionEditor
{
	SetActiveScene _target ;

	public override bool OnGUI()
	{
		_target = (SetActiveScene)target;

		EditField ("sceneReference");

		switch (_target.sceneReference) {
		case SetActiveScene.SceneReferenceOptions.SceneAtBuildIndex:
			EditField ("sceneAtBuildIndex");
			break;
		case SetActiveScene.SceneReferenceOptions.SceneAtIndex:
			EditField ("sceneAtIndex");
			break;
		case SetActiveScene.SceneReferenceOptions.SceneByName:
			EditField ("sceneByName");
			break;
		case SetActiveScene.SceneReferenceOptions.SceneByPath:
			EditField ("sceneByPath");
			break;
		case SetActiveScene.SceneReferenceOptions.SceneByGameObject:
			EditField ("sceneAtGameObject");
			break;
		default:
			throw new System.ArgumentOutOfRangeException ();
		}

		EditField("success");
		EditField("successEvent");
		EditField("sceneNotActivatedEvent");

		EditField("sceneFound");
		EditField("sceneNotFoundEvent");

		return GUI.changed;
	}
}

#endif