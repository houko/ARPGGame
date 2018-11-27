// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(GetSceneIsDirty))]
public class GetSceneIsDirtyCustomEditor : GetSceneActionBaseCustomEditor
{
	public override bool OnGUI()
	{
		bool changed = EditSceneReferenceField();

		EditField("isDirty");
		EditField("isDirtyEvent");
		EditField("isNotDirtyEvent");

		EditSceneReferenceResultFields();

		return GUI.changed || changed;
	}
}

#endif