// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using UnityEditor;

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(GetAnimatorCurrentStateInfoIsTag))]
public class GetAnimatorCurrentStateInfoIsTagActionEditor : OnAnimatorUpdateActionEditorBase
{

	public override bool OnGUI()
	{
		EditField("gameObject");
		EditField("layerIndex");
		EditField("tag");

		EditField("tagMatch");
		EditField("tagMatchEvent");
		EditField("tagDoNotMatchEvent");

		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
	}

}
