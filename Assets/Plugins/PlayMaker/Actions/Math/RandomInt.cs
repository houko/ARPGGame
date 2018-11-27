// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
// 'inclusiveMax' option added by MaDDoX (@brenoazevedo)

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Math)]
    [Tooltip("Sets an Integer Variable to a random value between Min/Max.")]
    public class RandomInt : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Minimum value for the random number.")]
        public FsmInt min;

        [RequiredField]
        [Tooltip("Maximim value for the random number.")]
        public FsmInt max;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the result in an Integer variable.")]
        public FsmInt storeResult;

        [Tooltip("Should the Max value be included in the possible results?")]
        public bool inclusiveMax;

        [Tooltip("Don't repeat the same value twice.")]
        public FsmBool noRepeat;

        private int randomIndex;
        private int lastIndex = -1;

        public override void Reset()
        {
            min = 0;
            max = 100;
            storeResult = null;
            // make default false to not break old behavior.
            inclusiveMax = false;
            noRepeat = true;
        }

        public override void OnEnter()
        {
            PickRandom();
            Finish();
        }

        void PickRandom()
        {
            if (noRepeat.Value && max.Value != min.Value)
            {
                do
                {
                    randomIndex = (inclusiveMax) ?
                    Random.Range(min.Value, max.Value + 1) :
                    Random.Range(min.Value, max.Value);
                } while (randomIndex == lastIndex);

                lastIndex = randomIndex;
                storeResult.Value = randomIndex;

            }
            else
            {
                randomIndex = (inclusiveMax) ?
                Random.Range(min.Value, max.Value + 1) :
                Random.Range(min.Value, max.Value);
                storeResult.Value = randomIndex;
            }
        }
    }
}
