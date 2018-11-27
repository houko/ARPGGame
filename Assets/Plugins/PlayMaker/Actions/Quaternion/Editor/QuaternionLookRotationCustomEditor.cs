// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;


[CustomActionEditor(typeof(QuaternionLookRotation))]
public class QuaternionLookRotationCustomEditor : QuaternionCustomEditorBase
{

    public override bool OnGUI()
    {
		EditField("direction");
		EditField("upVector");
		EditField("result");
		
		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
    }


}
