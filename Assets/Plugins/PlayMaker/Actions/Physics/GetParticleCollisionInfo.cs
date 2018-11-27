// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
// TODO: Needs to support: ParticlePhysicsExtensions.GetCollisionEvents

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Gets info on the last particle collision event. See Unity Particle System docs.")]
    public class GetParticleCollisionInfo : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [Tooltip("Get the GameObject hit.")]
        public FsmGameObject gameObjectHit;
        public override void Reset()
        {
            gameObjectHit = null;
        }

        private void StoreCollisionInfo()
        {
            gameObjectHit.Value = Fsm.ParticleCollisionGO;
        }

        public override void OnEnter()
        {
            StoreCollisionInfo();

            Finish();
        }
    }
}