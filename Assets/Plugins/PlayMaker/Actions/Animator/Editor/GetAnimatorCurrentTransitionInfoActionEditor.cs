// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using UnityEditor;

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(GetAnimatorCurrentTransitionInfo))]
public class GetAnimatorCurrentTransitionInfoActionEditor : OnAnimatorUpdateActionEditorBase
{

	public override bool OnGUI()
	{
		EditField("gameObject");
		EditField("layerIndex");
		EditField("name");

		EditField("nameHash");
		EditField("userNameHash");
		EditField("normalizedTime");

		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
	}

}
