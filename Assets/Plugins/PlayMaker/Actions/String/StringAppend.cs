// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Adds a String to the end of a String.")]
	public class StringAppend : FsmStateAction
	{
		[RequiredField]
        [Tooltip("Strings to add to.")]
        [UIHint(UIHint.Variable)]
		public FsmString stringVariable;

        [Tooltip("String to append")]
        public FsmString appendString;

		public override void Reset()
		{
		    stringVariable = null;
		    appendString = null;
		}

		public override void OnEnter()
		{
		    stringVariable.Value += appendString.Value;

		    Finish();
		}		
	}
}