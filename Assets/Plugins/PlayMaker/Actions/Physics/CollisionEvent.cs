// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Detect collisions between Game Objects that have RigidBody/Collider components.")]
	public class CollisionEvent : FsmStateAction
	{
	    [Tooltip("The GameObject to detect collisions on.")]
	    public FsmOwnerDefault gameObject;

        [Tooltip("The type of collision to detect.")]
		public CollisionType collision;
		
        [UIHint(UIHint.TagMenu)]
		[Tooltip("Filter by Tag.")]
        public FsmString collideTag;
		
        [Tooltip("Event to send if a collision is detected.")]
        public FsmEvent sendEvent;

		[UIHint(UIHint.Variable)]
        [Tooltip("Store the GameObject that collided with the Owner of this FSM.")]
		public FsmGameObject storeCollider;

		[UIHint(UIHint.Variable)]
        [Tooltip("Store the force of the collision. NOTE: Use Get Collision Info to get more info about the collision.")]
		public FsmFloat storeForce;

        // cached proxy component for callbacks
	    private PlayMakerProxyBase cachedProxy;

		public override void Reset()
		{
		    gameObject = null;
			collision = CollisionType.OnCollisionEnter;
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
	                case CollisionType.OnCollisionEnter:
	                    Fsm.HandleCollisionEnter = true;
	                    break;
	                case CollisionType.OnCollisionStay:
	                    Fsm.HandleCollisionStay = true;
	                    break;
	                case CollisionType.OnCollisionExit:
	                    Fsm.HandleCollisionExit = true;
	                    break;
	                case CollisionType.OnControllerColliderHit:
	                    Fsm.HandleControllerColliderHit = true;
	                    break;
	                case CollisionType.OnParticleCollision:
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
	            case CollisionType.OnCollisionEnter:
	                cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerCollisionEnter>(source);
	                break;
	            case CollisionType.OnCollisionStay:
	                cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerCollisionStay>(source);
	                break;
	            case CollisionType.OnCollisionExit:
	                cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerCollisionExit>(source);
	                break;
	            case CollisionType.OnParticleCollision:
	                cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerParticleCollision>(source);
	                break;
	            case CollisionType.OnControllerColliderHit:
	                cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerControllerColliderHit>(source);
	                break;
	        }
	    }

	    private void AddCallback()
	    {
	        if (cachedProxy == null)
	            return;

	        switch (collision)
	        {
                case CollisionType.OnCollisionEnter:
                    cachedProxy.AddCollisionEventCallback(CollisionEnter);
                    break;
                case CollisionType.OnCollisionStay:
                    cachedProxy.AddCollisionEventCallback(CollisionStay);
	                break;
                case CollisionType.OnCollisionExit:
                    cachedProxy.AddCollisionEventCallback(CollisionExit);
	                break;
                case CollisionType.OnParticleCollision:
                    cachedProxy.AddParticleCollisionEventCallback(ParticleCollision);
                    break;
	            case CollisionType.OnControllerColliderHit:
                    cachedProxy.AddControllerCollisionEventCallback(ControllerColliderHit);
	                break;
	        }
	    }

	    private void RemoveCallback()
	    {
	        if (cachedProxy == null)
	            return;

	        switch (collision)
	        {
	            case CollisionType.OnCollisionEnter:
	                cachedProxy.RemoveCollisionEventCallback(CollisionEnter);
	                break;
	            case CollisionType.OnCollisionStay:
                    cachedProxy.RemoveCollisionEventCallback(CollisionStay);
	                break;
	            case CollisionType.OnCollisionExit:
                    cachedProxy.RemoveCollisionEventCallback(CollisionExit);
	                break;
	            case CollisionType.OnParticleCollision:
	                cachedProxy.RemoveParticleCollisionEventCallback(ParticleCollision);
	                break;
	            case CollisionType.OnControllerColliderHit:
	                cachedProxy.RemoveControllerCollisionEventCallback(ControllerColliderHit);
	                break;
	        }
	    }

	    private void StoreCollisionInfo(Collision collisionInfo)
		{
			storeCollider.Value = collisionInfo.gameObject;
			storeForce.Value = collisionInfo.relativeVelocity.magnitude;
		}

		public override void DoCollisionEnter(Collision collisionInfo)
		{
            if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
                CollisionEnter(collisionInfo);
		}

	    public override void DoCollisionStay(Collision collisionInfo)
	    {
	        if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
	            CollisionStay(collisionInfo);
	    }

	    public override void DoCollisionExit(Collision collisionInfo)
	    {
	        if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
	            CollisionExit(collisionInfo);
	    }

        public override void DoControllerColliderHit(ControllerColliderHit collisionInfo)
	    {
	        if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
                ControllerColliderHit(collisionInfo);
	    }

	    public override void DoParticleCollision(GameObject other)
	    {
	        if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
	            ParticleCollision(other);
	    }

	    private void CollisionEnter(Collision collisionInfo)
	    {
	        if (collision == CollisionType.OnCollisionEnter)
	        {
                if (TagMatches(collideTag, collisionInfo))
	            {
	                StoreCollisionInfo(collisionInfo);
	                Fsm.Event(sendEvent);
	            }
	        }
	    }

	    private void CollisionStay(Collision collisionInfo)
		{
			if (collision == CollisionType.OnCollisionStay)
			{
			    if (TagMatches(collideTag, collisionInfo))
				{
					StoreCollisionInfo(collisionInfo);
					Fsm.Event(sendEvent);
				}
			}
		}

	    private void CollisionExit(Collision collisionInfo)
		{
			if (collision == CollisionType.OnCollisionExit)
			{
			    if (TagMatches(collideTag, collisionInfo))
				{
					StoreCollisionInfo(collisionInfo);
					Fsm.Event(sendEvent);
				}
			}
		}

	    private void ControllerColliderHit(ControllerColliderHit collisionInfo)
		{
			if (collision == CollisionType.OnControllerColliderHit)
			{
			    if (TagMatches(collideTag, collisionInfo))
				{
					if (storeCollider != null)
						storeCollider.Value = collisionInfo.gameObject;

					storeForce.Value = 0f; //TODO: impact force?
					Fsm.Event(sendEvent);
				}
			}
		}

	    private void ParticleCollision(GameObject other)
	    {
	        if (collision == CollisionType.OnParticleCollision)
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
	        return ActionHelpers.CheckPhysicsSetup(Fsm.GetOwnerDefaultTarget(gameObject));
		}
	}
}