// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets the value of an Integer Variable.")]
	public class SetIntValue : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("Int Variable to Set")]
		public FsmInt intVariable;

	    [RequiredField]
        [Tooltip("Int Value")]
	    public FsmInt intValue;

        [Tooltip("Repeat every frame.")]
	    public bool everyFrame;

		public override void Reset()
		{
			intVariable = null;
			intValue = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			intVariable.Value = intValue.Value;

		    if (!everyFrame)
		    {
		        Finish();
		    }		
		}

		public override void OnUpdate()
		{
			intVariable.Value = intValue.Value;
		}

#if UNITY_EDITOR
	    public override string AutoName()
	    {
	        return ActionHelpers.AutoNameSetVar(this, intVariable, intValue);
	    }
#endif
	}
}