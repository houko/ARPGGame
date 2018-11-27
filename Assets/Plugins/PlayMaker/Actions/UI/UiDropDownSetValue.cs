// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Set the selected value (zero based index) of the UI Dropdown Component")]
	public class UiDropDownSetValue : ComponentAction<UnityEngine.UI.Dropdown>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Dropdown))]
		[Tooltip("The GameObject with the UI DropDown component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The selected index of the dropdown (zero based index).")]
		public FsmInt value;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

	    private UnityEngine.UI.Dropdown dropDown;

		public override void Reset()
		{
			gameObject = null;
			value = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				dropDown = cachedComponent;
			}

			SetValue ();

			if (!everyFrame)
			{
				Finish ();
			}			
		}

		public override void OnUpdate()
		{
			SetValue ();
		}

	    private void SetValue()
		{
			if (dropDown==null)
			{
				return;
			}

			if (dropDown.value != value.Value)
			{
				dropDown.value = value.Value;
			}
		}
	}
}
#endif