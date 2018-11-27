// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using UnityEditor;

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(GetAnimatorIKGoal))]
public class GetAnimatorIKGoalActionEditor : OnAnimatorUpdateActionEditorBase
{

	public override bool OnGUI()
	{
		EditField("gameObject");
		EditField("iKGoal");
		EditField("goal");
		EditField("position");
		EditField("rotation");
		EditField("positionWeight");
		EditField("rotationWeight");
		
		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
	}

}
