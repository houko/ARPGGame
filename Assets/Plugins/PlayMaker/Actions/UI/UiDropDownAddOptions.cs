// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Add multiple options to the options of the Dropdown uGui Component")]
	public class UiDropDownAddOptions : ComponentAction<UnityEngine.UI.Dropdown>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Dropdown))]
		[Tooltip("The GameObject with the UI DropDown component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The Options.")]
		[CompoundArray("Options","Text","Image")]
		public FsmString[] optionText;
		[ObjectType(typeof(Sprite))]
		public FsmObject[] optionImage;


	    private UnityEngine.UI.Dropdown dropDown;
	    private List<UnityEngine.UI.Dropdown.OptionData> options;

		public override void Reset()
		{
			gameObject = null;
			optionText = new FsmString[1];
			optionImage = new FsmObject[1];
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				dropDown = cachedComponent;
			}

			DoAddOptions ();
			
			Finish();			
		}


	    private void DoAddOptions()
		{
			if (dropDown==null) return;
			
			options = new List<UnityEngine.UI.Dropdown.OptionData> ();

		    for (var i = 0; i < optionText.Length; i++)
		    {
		        var text = optionText[i];
		        options.Add(
		            new UnityEngine.UI.Dropdown.OptionData
		            {
		                text = text.Value,
		                image =  optionImage[i].RawValue as Sprite
		            }
		        );
		    }

		    dropDown.AddOptions (options);
		}
	}
}
#endif