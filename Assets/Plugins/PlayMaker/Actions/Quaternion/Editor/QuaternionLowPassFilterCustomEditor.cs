// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;


[CustomActionEditor(typeof(QuaternionLowPassFilter))]
public class QuaternionLowPassFilterCustomEditor : QuaternionCustomEditorBase
{

    public override bool OnGUI()
    {
		EditField("quaternionVariable");
		EditField("filteringFactor");
		
		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
    }


}
