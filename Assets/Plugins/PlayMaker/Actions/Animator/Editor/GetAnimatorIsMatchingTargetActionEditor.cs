// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using UnityEditor;

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(GetAnimatorIsMatchingTarget))]
public class GetAnimatorIsMatchingTargetActionEditor : OnAnimatorUpdateActionEditorBase
{

	public override bool OnGUI()
	{
		EditField("gameObject");
		EditField("isMatchingActive");
		EditField("matchingActivatedEvent");
		EditField("matchingDeactivedEvent");

		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
	}

}
