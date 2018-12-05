using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

namespace HutongGames.PlayMakerEditor{

	[CustomActionEditor(typeof (HutongGames.PlayMaker.Actions.EasyTouchQuickTwist))]
	public class EasyTouchQuickTwistInspector : CustomActionEditor {

		public override bool OnGUI(){
			
			EditField( "actionTriggering");
			EditField( "onOwner");
			EditField( "allowOverUIElement");
			EditField( "sendEvent");
			
			return GUI.changed;
		}
	}
}
