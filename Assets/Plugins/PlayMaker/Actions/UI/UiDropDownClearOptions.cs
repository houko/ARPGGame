// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Clear the list of options in a UI Dropdown Component")]
	public class UiDropDownClearOptions : ComponentAction<UnityEngine.UI.Dropdown>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Dropdown))]
		[Tooltip("The GameObject with the UI DropDown component.")]
		public FsmOwnerDefault gameObject;

	    private UnityEngine.UI.Dropdown dropDown;

		public override void Reset()
		{
			gameObject = null;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				dropDown = cachedComponent;
			}

			if (dropDown != null)
			{
				dropDown.ClearOptions ();
			}
			
			Finish();			
		}
	}
}
#endif