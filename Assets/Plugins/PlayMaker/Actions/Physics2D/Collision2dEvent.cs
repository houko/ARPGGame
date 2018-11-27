// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics2D)]
    [Tooltip("Detect collisions between Game Objects that have RigidBody2D/Collider2D components.")]
    public class Collision2dEvent : FsmStateAction
    {
        [Tooltip("The GameObject to detect collisions on.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("The type of collision to detect.")]
        public Collision2DType collision;

        [UIHint(UIHint.TagMenu)]
        [Tooltip("Filter by Tag.")]
        public FsmString collideTag;

        [Tooltip("Event to send if a collision is detected.")]
        public FsmEvent sendEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the GameObject that collided with the Owner of this FSM.")]
        public FsmGameObject storeCollider;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the force of the collision. NOTE: Use Get Collision 2D Info to get more info about the collision.")]
        public FsmFloat storeForce;

        // cached proxy component for callbacks
        private PlayMakerProxyBase cachedProxy;

        public override void Reset()
        {
            collision = Collision2DType.OnCollisionEnter2D;
            collideTag = "";
            sendEvent = null;
            storeCollider = null;
            storeForce = null;
        }

        public override void OnPreprocess()
        {
            if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
            {
                switch (collision)
                {
                    case Collision2DType.OnCollisionEnter2D:
                        Fsm.HandleCollisionEnter2D = true;
                        break;
                    case Collision2DType.OnCollisionStay2D:
                        Fsm.HandleCollisionStay2D = true;
                        break;
                    case Collision2DType.OnCollisionExit2D:
                        Fsm.HandleCollisionExit2D = true;
                        break;
                    case Collision2DType.OnParticleCollision:
                        Fsm.HandleParticleCollision = true;
                        break;
                }
            }
            else
            {
                // Add proxy components now if we can
                GetProxyComponent();
            }
        }

        public override void OnEnter()
        {
            if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
                return;

            if (cachedProxy == null)
                GetProxyComponent();

            AddCallback();

            gameObject.GameObject.OnChange += UpdateCallback;
        }

        public override void OnExit()
        {
            if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
                return;

            RemoveCallback();

            gameObject.GameObject.OnChange -= UpdateCallback;
        }

        private void UpdateCallback()
        {
            RemoveCallback();
            GetProxyComponent();
            AddCallback();
        }

        private void GetProxyComponent()
        {
            cachedProxy = null;
            var source = gameObject.GameObject.Value;
            if (source == null)
                return;

            switch (collision)
            {
                case Collision2DType.OnCollisionEnter2D:
                    cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerCollisionEnter2D>(source);
                    break;
                case Collision2DType.OnCollisionStay2D:
                    cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerCollisionStay2D>(source);
                    break;
                case Collision2DType.OnCollisionExit2D:
                    cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerCollisionExit2D>(source);
                    break;
                case Collision2DType.OnParticleCollision:
                    cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerParticleCollision>(source);
                    break;
            }
        }

        private void AddCallback()
        {
            if (cachedProxy == null)
                return;

            switch (collision)
            {
                case Collision2DType.OnCollisionEnter2D:
                    cachedProxy.AddCollision2DEventCallback(CollisionEnter2D);
                    break;
                case Collision2DType.OnCollisionStay2D:
                    cachedProxy.AddCollision2DEventCallback(CollisionStay2D);
                    break;
                case Collision2DType.OnCollisionExit2D:
                    cachedProxy.AddCollision2DEventCallback(CollisionExit2D);
                    break;
                case Collision2DType.OnParticleCollision:
                    cachedProxy.AddParticleCollisionEventCallback(ParticleCollision);
                    break;
            }
        }

        private void RemoveCallback()
        {
            if (cachedProxy == null)
                return;

            switch (collision)
            {
                case Collision2DType.OnCollisionEnter2D:
                    cachedProxy.RemoveCollision2DEventCallback(CollisionEnter2D);
                    break;
                case Collision2DType.OnCollisionStay2D:
                    cachedProxy.RemoveCollision2DEventCallback(CollisionStay2D);
                    break;
                case Collision2DType.OnCollisionExit2D:
                    cachedProxy.RemoveCollision2DEventCallback(CollisionExit2D);
                    break;
                case Collision2DType.OnParticleCollision:
                    cachedProxy.RemoveParticleCollisionEventCallback(ParticleCollision);
                    break;
            }
        }

        private void StoreCollisionInfo(Collision2D collisionInfo)
        {
            storeCollider.Value = collisionInfo.gameObject;
            storeForce.Value = collisionInfo.relativeVelocity.magnitude;
        }

        public override void DoCollisionEnter2D(Collision2D collisionInfo)
        {
            if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
                CollisionEnter2D(collisionInfo);
        }

        public override void DoCollisionStay2D(Collision2D collisionInfo)
        {
            if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
                CollisionStay2D(collisionInfo);
        }

        public override void DoCollisionExit2D(Collision2D collisionInfo)
        {
            if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
                CollisionExit2D(collisionInfo);
        }

        public override void DoParticleCollision(GameObject other)
        {
            if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
                ParticleCollision(other);
        }

        private void CollisionEnter2D(Collision2D collisionInfo)
        {
            if (collision == Collision2DType.OnCollisionEnter2D)
            {
                if (TagMatches(collideTag, collisionInfo))
                {
                    StoreCollisionInfo(collisionInfo);
                    Fsm.Event(sendEvent);
                }
            }
        }

        private void CollisionStay2D(Collision2D collisionInfo)
        {
            if (collision == Collision2DType.OnCollisionStay2D)
            {
                if (TagMatches(collideTag, collisionInfo))
                {
                    StoreCollisionInfo(collisionInfo);
                    Fsm.Event(sendEvent);
                }
            }
        }

        private void CollisionExit2D(Collision2D collisionInfo)
        {
            if (collision == Collision2DType.OnCollisionExit2D)
            {
                if (TagMatches(collideTag, collisionInfo))
                {
                    StoreCollisionInfo(collisionInfo);
                    Fsm.Event(sendEvent);
                }
            }
        }

        private void ParticleCollision(GameObject other)
        {
            if (collision == Collision2DType.OnParticleCollision)
            {
                if (TagMatches(collideTag, other))
                {
                    if (storeCollider != null)
                        storeCollider.Value = other;

                    storeForce.Value = 0f; //TODO: impact force?
                    Fsm.Event(sendEvent);
                }
            }
        }

        public override string ErrorCheck()
        {
            return ActionHelpers.CheckPhysics2dSetup(Fsm.GetOwnerDefaultTarget(gameObject));
        }
    }
}