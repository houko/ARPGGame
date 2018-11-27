// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(MoveGameObjectToScene))]
public class MoveGameObjectToSceneCustomEditor : GetSceneActionBaseCustomEditor
{
	public override bool OnGUI()
	{
		EditField ("gameObject");
		EditField("findRootIfNecessary");

		bool changed = EditSceneReferenceField();

		EditField ("success");
		EditField ("successEvent");
		EditField ("failureEvent");

		EditSceneReferenceResultFields();

		return GUI.changed || changed;
	}
}

#endif