// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using UnityEditor;

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(GetAnimatorPivot))]
public class GetAnimatorPivotActionEditor : OnAnimatorUpdateActionEditorBase
{

	public override bool OnGUI()
	{
		EditField("gameObject");
		EditField("pivotWeight");
		EditField("pivotPosition");

		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
	}

}
