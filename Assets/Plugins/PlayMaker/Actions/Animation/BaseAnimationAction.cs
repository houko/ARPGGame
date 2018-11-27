using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    // Base class for logging actions 
    public abstract class BaseAnimationAction : ComponentAction<Animation>
    {
        //#if UNITY_EDITOR

        public override void OnActionTargetInvoked(object targetObject)
        {
            var animClip = targetObject as AnimationClip;
            if (animClip == null) return;
            
            var animationComponent = Owner.GetComponent<Animation>();
            if (animationComponent != null)
            {
                animationComponent.AddClip(animClip, animClip.name);
            }
        }

        //#endif
    }
}