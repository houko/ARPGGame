// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using UnityEditor;

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(GetAnimatorBody))]
public class GetAnimatorBodyActionEditor : OnAnimatorUpdateActionEditorBase
{

	public override bool OnGUI()
	{
		EditField("gameObject");
		EditField("bodyPosition");
		EditField("bodyRotation");
		EditField("bodyGameObject");

		EditField("everyFrame");

		return GUI.changed;
	}

}
