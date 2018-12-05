using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

namespace HutongGames.PlayMakerEditor{

	[CustomActionEditor(typeof (HutongGames.PlayMaker.Actions.EasyTouchQuickPinch))]
	public class EasyTouchQuickPinchInspector : CustomActionEditor {

		public override bool OnGUI(){

			EditField( "actionTriggering");
			EditField( "onOwner");
			EditField( "allowOverUIElement");
			EditField( "sendEvent");

			return GUI.changed;
		}
	}
}
