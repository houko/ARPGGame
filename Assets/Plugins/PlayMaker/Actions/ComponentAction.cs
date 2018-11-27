// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

// Unity 5.1 introduced a new networking library. 
// Unless we define PLAYMAKER_LEGACY_NETWORK old network actions are disabled
#if !(UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || PLAYMAKER_LEGACY_NETWORK)
#define UNITY_NEW_NETWORK
#endif

// Some platforms do not support networking (at least the old network library)
#if (UNITY_SWITCH || UNITY_FLASH || UNITY_NACL || UNITY_METRO || UNITY_WP8 || UNITY_WIIU || UNITY_PSM || UNITY_WEBGL || UNITY_PS3 || UNITY_PS4 || UNITY_XBOXONE)
#define PLATFORM_NOT_SUPPORTED
#endif

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    // Base class for actions that access a Component on a GameObject.
    // Caches the component for performance
    public abstract class ComponentAction<T> : FsmStateAction where T : Component
    {
		/// <summary>
		/// The cached GameObject. Call UpdateCache() first
		/// </summary>
        protected GameObject cachedGameObject;

		/// <summary>
		/// The cached component. Call UpdateCache() first
		/// </summary>
        protected T cachedComponent;

        protected Rigidbody rigidbody
        {
            get { return cachedComponent as Rigidbody; }
        }

        protected Rigidbody2D rigidbody2d
        {
            get { return cachedComponent as Rigidbody2D; }
        }

        protected Renderer renderer
        {
            get { return cachedComponent as Renderer; }
        }

        protected Animation animation
        {
            get { return cachedComponent as Animation; }
        }

        protected AudioSource audio
        {
            get { return cachedComponent as AudioSource; }
        }

        protected Camera camera
        {
            get { return cachedComponent as Camera; }
        }

		#if UNITY_2017_2_OR_NEWER
		#pragma warning disable CS0618 
        #endif
        protected GUIText guiText
        {
            get { return cachedComponent as GUIText; }
        }

        protected GUITexture guiTexture
        {
            get { return cachedComponent as GUITexture; }
        }
        #if UNITY_2017_2_OR_NEWER
        #pragma warning restore CS0618 
		#endif

        protected Light light
        {
            get { return cachedComponent as Light; }
        }

#if !(PLATFORM_NOT_SUPPORTED || UNITY_NEW_NETWORK || PLAYMAKER_NO_NETWORK)
        protected NetworkView networkView
        {
            get { return cachedComponent as NetworkView; }
        }
#endif

        // Check that the GameObject is the same
        // and that we have a component reference cached
        protected bool UpdateCache(GameObject go)
        {
            if (go == null) return false;

            if (cachedComponent == null || cachedGameObject != go)
            {
                cachedComponent = go.GetComponent<T>();
                cachedGameObject = go;

                if (cachedComponent == null)
                {
                    LogWarning("Missing component: " + typeof(T).FullName + " on: " + go.name);
                }
            }

            return cachedComponent != null;
        }

        // Same as UpdateCache, but adds a new component if missing
        protected bool UpdateCacheAddComponent(GameObject go)
        {
            if (go == null) return false;

            if (cachedComponent == null || cachedGameObject != go)
            {
                cachedComponent = go.GetComponent<T>();
                cachedGameObject = go;

                if (cachedComponent == null)
                {
                    cachedComponent = go.AddComponent<T>();
                    cachedComponent.hideFlags = HideFlags.DontSaveInEditor;                       
                }
            }

            return cachedComponent != null;
        }

        protected void SendEvent(FsmEventTarget eventTarget, FsmEvent fsmEvent)
        {
            Fsm.Event(cachedGameObject, eventTarget, fsmEvent);
        }
    }
}