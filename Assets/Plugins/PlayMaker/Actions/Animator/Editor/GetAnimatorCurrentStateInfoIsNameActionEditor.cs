// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using UnityEditor;

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(GetAnimatorCurrentStateInfoIsName))]
public class GetAnimatorCurrentStateInfoIsNameActionEditor : OnAnimatorUpdateActionEditorBase
{

	public override bool OnGUI()
	{
		EditField("gameObject");
		EditField("layerIndex");
		EditField("name");

		EditField("isMatching");
		EditField("nameMatchEvent");
		EditField("nameDoNotMatchEvent");

		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
	}

}
