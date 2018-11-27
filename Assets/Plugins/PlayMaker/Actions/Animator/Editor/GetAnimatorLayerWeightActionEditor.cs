// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using UnityEditor;

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(GetAnimatorLayerWeight))]
public class GetAnimatorLayerWeightActionEditor : OnAnimatorUpdateActionEditorBase
{

	public override bool OnGUI()
	{
		EditField("gameObject");
		EditField("layerIndex");
		EditField("layerWeight");

		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
	}

}
