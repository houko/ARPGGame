// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
// http://hutonggames.com/playmakerforum/index.php?topic=8452

using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the EventSystem's currently select GameObject.")]
	public class UiSetSelectedGameObject : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The GameObject to select.")]
		public FsmGameObject gameObject;

		public override void Reset()
		{
			gameObject = null;
		}

		public override void OnEnter()
		{
		    DoSetSelectedGameObject();
			
			Finish();	
		}

	    private void DoSetSelectedGameObject()
		{
			EventSystem.current.SetSelectedGameObject (gameObject.Value);
		}

	}
}
