// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
	[Tooltip("Set an item in an Array Variable in another FSM.")]
    public class SetFsmArrayItem : BaseFsmVariableIndexAction
	{
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object.")]
		public FsmString fsmName;
		
		[RequiredField]
        [UIHint(UIHint.FsmArray)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

        [Tooltip("The index into the array.")]
        public FsmInt index;

        [RequiredField]
        //[MatchElementType("array")] TODO
        [Tooltip("Set the value of the array at the specified index.")]
        public FsmVar value;
		
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

        public override void Reset()
		{
			gameObject = null;
			fsmName = "";
			value = null;
		}
		
		public override void OnEnter()
		{
            DoSetFsmArray();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}

	    private void DoSetFsmArray()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (!UpdateCache(go, fsmName.Value))
		    {
		        return;
		    }
         		
			var fsmArray = fsm.FsmVariables.GetFsmArray(variableName.Value);
            if (fsmArray != null)
			{
                if (index.Value < 0 || index.Value >= fsmArray.Length)
                {
                    Fsm.Event(indexOutOfRange);
                    Finish();
                    return;
                }

			    if (fsmArray.ElementType == value.NamedVar.VariableType)
			    {
                    value.UpdateValue();
			        fsmArray.Set(index.Value, value.GetValue());
			    }
			    else
			    {
			        LogWarning("Incompatible variable type: " + variableName.Value);
			    }
			}
			else
			{
                DoVariableNotFound(variableName.Value);
			}			
		}
		
		public override void OnUpdate()
		{
			DoSetFsmArray();
		}
		
	}
}