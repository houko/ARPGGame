// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using UnityEditor;

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(GetAnimatorRoot))]
public class GetAnimatorRootActionEditor : OnAnimatorUpdateActionEditorBase
{

	public override bool OnGUI()
	{
		EditField("gameObject");
		EditField("rootPosition");
		EditField("rootRotation");
		
		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
	}

}
