// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends Events based on the comparison of 2 Colors.")]
	public class ColorCompare : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The first Color.")]
		public FsmColor color1;

		[RequiredField]
        [Tooltip("The second Color.")]
		public FsmColor color2;

		[RequiredField]
        [Tooltip("Tolerance of test, to test for 'almost equals' or to ignore small floating point rounding differences.")]
		public FsmFloat tolerance;

		[Tooltip("Event sent if Color 1 equals Color 2 (within Tolerance)")]
		public FsmEvent equal;
		
	    [Tooltip("Event sent if Color 1 does not equal Color 2 (within Tolerance)")]
	    public FsmEvent notEqual;		

        [Tooltip("Repeat every frame. Useful if the variables are changing and you're waiting for a particular result.")]
        public bool everyFrame;

		public override void Reset()
		{
		    color1 = Color.white;
		    color2 = Color.white;
			tolerance = 0f;
			equal = null;
		    notEqual = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoCompare();
			
			if (!everyFrame)
			{
			    Finish();
			}
		}

		public override void OnUpdate()
		{
			DoCompare();
		}

	    private void DoCompare()
	    {
            // alternatively convert to Vector4 and check magnitude > tolerance

	        if (Mathf.Abs(color1.Value.r - color2.Value.r) > tolerance.Value ||
	            Mathf.Abs(color1.Value.g - color2.Value.g) > tolerance.Value ||
	            Mathf.Abs(color1.Value.b - color2.Value.b) > tolerance.Value ||
	            Mathf.Abs(color1.Value.a - color2.Value.a) > tolerance.Value)
	        {
	            Fsm.Event(notEqual);
	        }
	        else
	        {
	            Fsm.Event(equal);
	        }			
		}

		public override string ErrorCheck()
		{
		    if (FsmEvent.IsNullOrEmpty(equal) && FsmEvent.IsNullOrEmpty(notEqual))
		    {
		        return "Action sends no events!";
		    }

		    return "";
		}
	}
}