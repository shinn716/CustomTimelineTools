using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Shinn.Timelinie
{
    public class CustomTimelineEventManager : MonoBehaviour
    {
        [SerializeField] UnityEvent<int> onStartEvents_INT = new UnityEvent<int>();
        [SerializeField] UnityEvent<float> onStartEvents_FLOAT = new UnityEvent<float>();
        [SerializeField] UnityEvent<string> onStartEvents_STRING = new UnityEvent<string>();
        
        // Message method
        private void StartEvent(object[] objs)
        {
            InputTypeConvert_START(objs[0], objs[1]);
        }

        // public 
        public void ClearOnStartEvents_INT()
        {
            onStartEvents_INT = new UnityEvent<int>();
        }

        public void ClearOnStartEvents_FLOAT()
        {
            onStartEvents_FLOAT = new UnityEvent<float>();
        }

        public void ClearOnStartEvents_STRING()
        {
            onStartEvents_STRING = new UnityEvent<string>();
        }

        // Private
        private void InputTypeConvert_START(object type, object input)
        {
            switch (type.ToString())
            {
                default:
                    onStartEvents_STRING.Invoke(input.ToString());
                    break;
                case "Int":
                    onStartEvents_INT.Invoke(int.Parse(input.ToString()));
                    break;
                case "Float":
                    onStartEvents_FLOAT.Invoke(float.Parse(input.ToString()));
                    break;
            }
        }
    }
}