// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Get the XY channels of a Vector2 Variable and store them in Float Variables.")]
	public class GetVector2XY : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector2 source")]
		public FsmVector2 vector2Variable;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The x component")]
		public FsmFloat storeX;		
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The y component")]
		public FsmFloat storeY;	
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		public override void Reset()
		{
			vector2Variable = null;
			storeX = null;
			storeY = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetVector2XYZ();
			
			if(!everyFrame)
				Finish();
		}
		
		public override void OnUpdate ()
		{
			DoGetVector2XYZ();
		}
		
		void DoGetVector2XYZ()
		{
			if (vector2Variable == null) return;
			
			if (storeX != null)
				storeX.Value = vector2Variable.Value.x;

			if (storeY != null)
				storeY.Value = vector2Variable.Value.y;

		}
	}
}