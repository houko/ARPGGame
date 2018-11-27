// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout Toolbar. NOTE: Arrays must be the same length as NumButtons or empty.")]
	public class GUILayoutToolbar : GUILayoutAction
	{
        [Tooltip("The number of buttons in the toolbar")]
		public FsmInt numButtons;

        [Tooltip("Store the index of the selected button in an Integer Variable")]
		[UIHint(UIHint.Variable)]
		public FsmInt selectedButton;

        [Tooltip("Event to send when each button is pressed.")]
		public FsmEvent[] buttonEventsArray;

        [Tooltip("Image to use on each button.")]
		public FsmTexture[] imagesArray;

        [Tooltip("Text to use on each button.")]
		public FsmString[] textsArray;

        [Tooltip("Tooltip to use for each button.")]
		public FsmString[] tooltipsArray;

        [Tooltip("A named GUIStyle to use for the toolbar buttons. Default is Button.")]
		public FsmString style;

        [Tooltip("Update the content of the buttons every frame. Useful if the buttons are using variables that change.")]
	    public bool everyFrame;

	    private GUIContent[] contents;

		public GUIContent[] Contents
		{
			get 
			{
				if (contents == null)
				{
				    SetButtonsContent();
				}

			    return contents;
			}
		}

	    private void SetButtonsContent()
	    {
	        if (contents == null)
	        {
	            contents = new GUIContent[numButtons.Value];
	        }

	        for (var i = 0; i < numButtons.Value; i++)
	            contents[i] = new GUIContent();

	        for (var i = 0; i < imagesArray.Length; i++)
	            contents[i].image = imagesArray[i].Value;

	        for (var i = 0; i < textsArray.Length; i++)
	            contents[i].text = textsArray[i].Value;

	        for (var i = 0; i < tooltipsArray.Length; i++)
	            contents[i].tooltip = tooltipsArray[i].Value;
	    }

	    public override void Reset()
		{
			base.Reset();
			numButtons = 0;
			selectedButton = null;
			buttonEventsArray = new FsmEvent[0];
			imagesArray = new FsmTexture[0];
			tooltipsArray = new FsmString[0];
			style = "Button";
	        everyFrame = false;
		}
		
		public override void OnEnter()
		{
			var error = ErrorCheck();
			
			if (!string.IsNullOrEmpty(error))
			{
				LogError(error);
				Finish();
			}			
		}
		
		public override void OnGUI()
		{
		    if (everyFrame)
		    {
		        SetButtonsContent();
		    }

			var guiChanged = GUI.changed;
			GUI.changed = false;
			
			selectedButton.Value = GUILayout.Toolbar(selectedButton.Value, Contents, style.Value, LayoutOptions);
			
			if (GUI.changed)
			{
				if (selectedButton.Value < buttonEventsArray.Length)
				{
					Fsm.Event(buttonEventsArray[selectedButton.Value]);
					GUIUtility.ExitGUI();
				}
			}
			else
			{
				GUI.changed = guiChanged;
			}
		}
		
		public override string ErrorCheck ()
		{
			var error = "";
			
			if (imagesArray.Length > 0 && imagesArray.Length != numButtons.Value)
				error += "Images array doesn't match NumButtons.\n";
			if (textsArray.Length > 0 && textsArray.Length != numButtons.Value)
				error += "Texts array doesn't match NumButtons.\n";
			if (tooltipsArray.Length > 0 && tooltipsArray.Length != numButtons.Value)
				error += "Tooltips array doesn't match NumButtons.\n";
				
			return error;
		}
	}
}