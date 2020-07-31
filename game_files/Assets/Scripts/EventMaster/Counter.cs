using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class Counter : MonoBehaviour
    {
        private int _counterValue = 0;

        public Counter(int counterValue)
        {
            _counterValue = counterValue;
        }

        public void AddToCounter(int counter)
        {
            _counterValue += counter;
        }

        public int ReturnCounter()
        {
            return _counterValue;
        }

        public void NullifyCounter()
        {
            _counterValue = 0;
        }
    }
}
