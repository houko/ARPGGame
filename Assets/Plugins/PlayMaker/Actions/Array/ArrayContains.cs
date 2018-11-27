// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Check if an Array contains a value. Optionally get its index.")]
	public class ArrayContains : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		[RequiredField]
		[MatchElementType("array")]
		[Tooltip("The value to check against in the array.")]
		public FsmVar value;
			
		[ActionSection("Result")]

		[Tooltip("The index of the value in the array.")]
		[UIHint(UIHint.Variable)]
		public FsmInt index;

		[Tooltip("Store in a bool whether it contains that element or not (described below)")]
		[UIHint(UIHint.Variable)]
		public FsmBool isContained;

		[Tooltip("Event sent if the array contains that element (described below)")]
		public FsmEvent isContainedEvent;

		[Tooltip("Event sent if the array does not contains that element (described below)")]
		public FsmEvent isNotContainedEvent;

		public override void Reset ()
		{
			array = null;
			value = null;

			index = null;

			isContained = null;
			isContainedEvent = null;
			isNotContainedEvent = null;
		}

		// Code that runs on entering the state.
		public override void OnEnter ()
		{
			DoCheckContainsValue ();
			Finish ();
		}

        private void DoCheckContainsValue()
        {
            value.UpdateValue();

			int _id =-1;
			
			if (value.GetValue() == null ||  value.GetValue().Equals(null))
			{
				_id =Array.FindIndex(array.Values,x => x==null || x.Equals(null));
			}else{
				
				_id = Array.IndexOf(array.Values, value.GetValue());
			}

            var _iscontained = _id != -1;
            isContained.Value = _iscontained;
            index.Value = _id;
            if (_iscontained)
            {
                Fsm.Event(isContainedEvent);
            }
            else
            {
                Fsm.Event(isNotContainedEvent);
            }
        }
	}
}
