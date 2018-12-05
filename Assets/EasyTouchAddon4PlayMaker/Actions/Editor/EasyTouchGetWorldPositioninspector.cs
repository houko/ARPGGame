using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

namespace HutongGames.PlayMakerEditor{
	
	[CustomActionEditor(typeof (HutongGames.PlayMaker.Actions.EasyTouchGetTouchToWorldPoint))]
	public class EasyTouchGetWorldPositioninspector : CustomActionEditor {

		public override bool OnGUI(){

			EasyTouchGetTouchToWorldPoint t = (EasyTouchGetTouchToWorldPoint)target;

			EditField( "depthType");

			switch( t.depthType){
			case EasyTouchGetTouchToWorldPoint.DepthType.Value:
				EditField( "z");
				break;
			case EasyTouchGetTouchToWorldPoint.DepthType.GameObjectReference:
				EditField( "gameObjectReference");
				break;
			case EasyTouchGetTouchToWorldPoint.DepthType.Position:
				EditField( "position");
				break;
			}

			EditField("worldPosition");
			return GUI.changed;
		}
	}
}