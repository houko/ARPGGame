// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the source image sprite of a UI Image component.")]
	public class UiImageGetSprite : ComponentAction<UnityEngine.UI.Image>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Image))]
		[Tooltip("The GameObject with the UI Image component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The source sprite of the UI Image component.")]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(Sprite))]
		public FsmObject sprite;

	    private UnityEngine.UI.Image image;

		public override void Reset()
		{
			gameObject = null;
			sprite = null;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				image = cachedComponent;
			}
			
			DoSetImageSourceValue();

			Finish();
		}

	    private void DoSetImageSourceValue()
		{
			if (image!=null)
			{
				sprite.Value = image.sprite;
			}
		}
	}
}