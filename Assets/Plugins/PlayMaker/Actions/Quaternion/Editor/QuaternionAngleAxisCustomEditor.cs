// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;


[CustomActionEditor(typeof(QuaternionAngleAxis))]
public class QuaternionAngleAxisCustomEditor : QuaternionCustomEditorBase
{

    public override bool OnGUI()
    {
		EditField("angle");
		EditField("axis");
		EditField("result");
		
		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
    }


}
