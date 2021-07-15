using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Shinn.Timelinie
{
    public class CustomTimelineEventManager : MonoBehaviour
    {
        private enum Status
        {
            Start,
            End
        }
        
        [SerializeField] UnityEvent<int> onStartEvents_INT = new UnityEvent<int>();
        [SerializeField] UnityEvent<float> onStartEvents_FLOAT = new UnityEvent<float>();
        [SerializeField] UnityEvent<string> onStartEvents_STRING = new UnityEvent<string>();
        
        [SerializeField] UnityEvent<int> onEndEvents_INT = new UnityEvent<int>();
        [SerializeField] UnityEvent<float> onEndEvents_FLOAT = new UnityEvent<float>();
        [SerializeField] UnityEvent<string> onEndEvents_STRING = new UnityEvent<string>();

        // Message method
        private void StartEvent(object[] objs)
        {
            StatusProcess(Status.Start, objs[0], objs[1]);
        }

        private void EndEvent(object[] objs)
        {
            StatusProcess(Status.End, objs[0], objs[1]);
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
        
        public void ClearOnEndEvents_INT()
        {
            onEndEvents_INT = new UnityEvent<int>();
        }

        public void ClearOnEndEvents_FLOAT()
        {
            onEndEvents_FLOAT = new UnityEvent<float>();
        }

        public void ClearOnEndEvents_STRING()
        {
            onEndEvents_STRING = new UnityEvent<string>();
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

        private void InputTypeConvert_END(object type, object input)
        {
            switch (type.ToString())
            {
                default:
                    onEndEvents_STRING.Invoke(input.ToString());
                    break;
                case "Int":
                    onEndEvents_INT.Invoke(int.Parse(input.ToString()));
                    break;
                case "Float":
                    onEndEvents_FLOAT.Invoke(float.Parse(input.ToString()));
                    break;
            }
        }

        private void StatusProcess(Status status, object type, object input)
        {
            switch (status)
            {
                default:
                    break;
                case Status.Start:
                    InputTypeConvert_START(type, input);
                    break;
                case Status.End:
                    InputTypeConvert_END(type, input);
                    break;
            }
        }
    }
}