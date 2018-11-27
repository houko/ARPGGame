// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Adds a text area to the action list. NOTE: Doesn't do anything, just for notes...")]
	public class Comment : FsmStateAction
	{
		[UIHint(UIHint.Comment)]
		public string comment;

		public override void Reset()
		{
			comment = "";
		}

		public override void OnEnter()
		{
			Finish();
		}

#if UNITY_EDITOR
	    public override string AutoName()
	    {
	        return ": " + comment.Replace("\n","  ");
	    }
#endif
	}
}