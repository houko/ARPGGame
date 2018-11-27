// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using UnityEditor;

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(GetAnimatorDelta))]
public class GetAnimatorDeltaActionEditor : OnAnimatorUpdateActionEditorBase
{

	public override bool OnGUI()
	{
		EditField("gameObject");
		EditField("deltaPosition");
		EditField("deltaRotation");
		
		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
	}

}
