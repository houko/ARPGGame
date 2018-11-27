// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Get a Random item from an Array.")]
    public class ArrayGetRandom : FsmStateAction
    {
        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The Array to use.")]
        public FsmArray array;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the value in a variable.")]
        [MatchElementType("array")]
        public FsmVar storeValue;

        [Tooltip("The index of the value in the array.")]
        [UIHint(UIHint.Variable)]
        public FsmInt index;

        [Tooltip("Don't get the same item twice in a row.")]
        public FsmBool noRepeat;

        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;

        private int randomIndex;
        private int lastIndex = -1;

        public override void Reset()
        {
            array = null;
            storeValue = null;
            index = null;
            everyFrame = false;
            noRepeat = false;
        }

        // Code that runs on entering the state.
        public override void OnEnter()
        {
            DoGetRandomValue();

            if (!everyFrame)
            {
                Finish();
            }

        }

        public override void OnUpdate()
        {
            DoGetRandomValue();
        }

        private void DoGetRandomValue()
        {
            if (storeValue.IsNone)
            {
                return;
            }

            if (!noRepeat.Value || array.Length == 1)
            {
                randomIndex = Random.Range(0, array.Length);
            }
            else
            {
                do
                {
                    randomIndex = Random.Range(0, array.Length);
                } while (randomIndex == lastIndex);

                lastIndex = randomIndex;
            }

            index.Value = randomIndex;
            storeValue.SetValue(array.Get(index.Value));
        }


    }
}


