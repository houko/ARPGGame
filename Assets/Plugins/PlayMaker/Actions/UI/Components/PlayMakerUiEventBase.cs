#if !PLAYMAKER_NO_UI

using System;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker
{
    public abstract class PlayMakerUiEventBase : MonoBehaviour
    {
        public List<PlayMakerFSM> targetFsms = new List<PlayMakerFSM>();

        [NonSerialized]
        protected bool initialized;

        public void AddTargetFsm(PlayMakerFSM fsm)
        {
            if (!TargetsFsm(fsm))
            {
                targetFsms.Add(fsm);
            }

            Initialize();
        }

        private bool TargetsFsm(PlayMakerFSM fsm)
        {
            for (var i = 0; i < targetFsms.Count; i++)
            {
                var targetFsm = targetFsms[i];
                if (fsm == targetFsm)
                    return true;
            }

            return false;
        }

        protected void OnEnable()
        {
            Initialize();
        }

        public void PreProcess()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            initialized = true;
        }

        protected void SendEvent(FsmEvent fsmEvent)
        {
            for (var i = 0; i < targetFsms.Count; i++)
            {
                var targetFsm = targetFsms[i];
                targetFsm.Fsm.Event(gameObject, fsmEvent);
            }
        }

    }
}

#endif