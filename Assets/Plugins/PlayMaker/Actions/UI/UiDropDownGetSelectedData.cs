// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Get the selected value (zero based index), sprite and text from a UI Dropdown Component")]
	public class UiDropDownGetSelectedData :  ComponentAction<UnityEngine.UI.Dropdown>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Dropdown))]
		[Tooltip("The GameObject with the UI DropDown component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The selected index of the dropdown (zero based index).")]
		[UIHint(UIHint.Variable)]
		public FsmInt index;

		[Tooltip("The selected text.")]
		[UIHint(UIHint.Variable)]
		public FsmString getText;

		[ObjectType(typeof(Sprite))]
		[Tooltip("The selected text.")]
		[UIHint(UIHint.Variable)]
		public FsmObject getImage;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

	    private UnityEngine.UI.Dropdown dropDown;

		public override void Reset()
		{
			gameObject = null;
			index = null;
		    getText = null;
		    getImage = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
		    var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (UpdateCache(go))
		    {
		        dropDown = cachedComponent;
		    }

			GetValue ();

			if (!everyFrame)
			{
				Finish ();
			}			
		}

		public override void OnUpdate()
		{
			GetValue ();
		}

	    private void GetValue()
		{
			if (dropDown==null)
			{
				return;
			}

			if (!index.IsNone)
			{
			    index.Value = dropDown.value;
			}

			if (!getText.IsNone )
			{
			    getText.Value = dropDown.options [dropDown.value].text;
			}

			if (!getImage.IsNone )
			{
			    getImage.Value = dropDown.options [dropDown.value].image;
			}
		}
	}
}
#endif