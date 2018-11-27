// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	public abstract class QuaternionBaseAction : FsmStateAction
	{
		public enum everyFrameOptions {Update,FixedUpdate,LateUpdate};
		
		[Tooltip("Repeat every frame. Useful if any of the values are changing.")]
		public bool everyFrame;
		
		[Tooltip("Defines how to perform the action when 'every Frame' is enabled.")]
		public everyFrameOptions everyFrameOption;
		
		public override void Awake()
	    {
	        if (everyFrame)
		    {
		        switch (everyFrameOption)
		        {
		            case everyFrameOptions.FixedUpdate:
		                Fsm.HandleFixedUpdate = true;
		                break;
		            case everyFrameOptions.LateUpdate:
		                Fsm.HandleLateUpdate = true;
		                break;
		        }
			}	
	    }
		
	}
}

