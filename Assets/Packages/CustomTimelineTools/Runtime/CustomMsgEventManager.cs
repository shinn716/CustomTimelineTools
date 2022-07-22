using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinn.Timelinie
{
    [ExecuteInEditMode]
    public class CustomMsgEventManager : MonoBehaviour
    {
        [SerializeField] IntEvent intEvents;
        [SerializeField] FloatEvent floatEvents;
        [SerializeField] StringEvent stringEvents;
        
        // Message method
        private void StartEvent(object[] objs)
        {
            InputTypeConvert_START(objs[0], objs[1]);
        }

        // public 
        public void ClearOnStartEvents_INT()
        {
            intEvents = new IntEvent();
        }

        public void ClearOnStartEvents_FLOAT()
        {
            floatEvents = new FloatEvent();
        }

        public void ClearOnStartEvents_STRING()
        {
            stringEvents = new StringEvent();
        }

        // Private
        private void InputTypeConvert_START(object type, object input)
        {
            switch (type.ToString())
            {
                default:
                    stringEvents.Invoke(input.ToString());
                    break;
                case "Int":
                    intEvents.Invoke(int.Parse(input.ToString()));
                    break;
                case "Float":
                    floatEvents.Invoke(float.Parse(input.ToString()));
                    break;
            }
        }
    }
}