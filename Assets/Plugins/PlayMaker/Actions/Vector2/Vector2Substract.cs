// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Subtracts a Vector2 value from a Vector2 variable.")]
	public class Vector2Subtract : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Vector2 operand")]
		public FsmVector2 vector2Variable;
		[RequiredField]
		[Tooltip("The vector2 to subtract with")]
		public FsmVector2 subtractVector;
		
		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			vector2Variable = null;
			subtractVector = new FsmVector2 { UseVariable = true };
			everyFrame = false;
		}

		public override void OnEnter()
		{
			vector2Variable.Value = vector2Variable.Value - subtractVector.Value;
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			vector2Variable.Value = vector2Variable.Value - subtractVector.Value;
		}
	}
}

