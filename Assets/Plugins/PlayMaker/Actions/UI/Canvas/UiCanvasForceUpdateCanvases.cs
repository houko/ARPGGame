// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Force all canvases to update their content.\n" +
	         "Code that relies on up-to-date layout or content can call this method to ensure it before executing code that relies on it.")]
	public class UiCanvasForceUpdateCanvases: FsmStateAction
	{

		public override void OnEnter()
		{
			Canvas.ForceUpdateCanvases();

			Finish();
		}
	}
}