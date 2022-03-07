using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Playables;

namespace Shinn.Timelinie
{
    [Serializable]
    public class CustomEventControlPlayable : PlayableBehaviour
    {
        public class MethodData
        {
            public MethodData(Behaviour _behaviour, MethodInfo _methodInfo)
            {
                methodInfo = _methodInfo;
                behaviour = _behaviour;
            }

            public void Clear()
            {
                methodInfo = null;
                behaviour = null;
            }

            public MethodInfo methodInfo;
            public Behaviour behaviour;
        }

        public enum ParameterType
        {
            Null,
            Void,
            Int,
            Float,
            String
        }

        public GameObject targetEventmanager;
        public string HandlerKey;

        [SerializeField]
        public ParameterType type;
        [SerializeField]
        public string input_str;
        [SerializeField]
        public int input_int;
        [SerializeField]
        public float input_float;

        [SerializeField]
        public bool useClipDuring;

        private bool startOnce = false;
        private MethodData methodData;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (!Application.isPlaying)
                return;

            if (targetEventmanager == null)
                return;

            if (startOnce)
                return;

            startOnce = true;

            if (useClipDuring)
                input_float = (float)playable.GetDuration();

            methodData = GetInvocationInfo(HandlerKey);

            switch (type)
            {
                default:
                    break;
                case ParameterType.Void:
                    methodData.methodInfo.Invoke(methodData.behaviour, null);
                    break;
                case ParameterType.Int:
                    methodData.methodInfo.Invoke(methodData.behaviour, new[] { (object) input_int });
                    break;
                case ParameterType.Float:
                    methodData.methodInfo.Invoke(methodData.behaviour, new[] { (object) input_float });
                    break;
                case ParameterType.String:
                    methodData.methodInfo.Invoke(methodData.behaviour, new[] { (object) input_str });
                    break;
            }
        }
        
        private MethodData GetInvocationInfo(string methodKey)
        {
            Behaviour targetBehaviour = null;
            string methodName = null;
            GetBehaviourAndMethod(methodKey, ref targetBehaviour, ref methodName);
            
            if (targetBehaviour != null)
            {
                bool withParameter = type == ParameterType.Void ? false : true;

                //get the method info
                var methodInfo = targetBehaviour
                    .GetType()
                    .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(m => m.Name == methodName && m.ReturnType == typeof(void) &&
                                         m.GetParameters().Length == (withParameter ? 1 : 0));
                return new MethodData(targetBehaviour, methodInfo);
            }

            return null;
        }
        

        private void GetBehaviourAndMethod(string key, ref Behaviour targetBehaviour,
               ref string methodName)
        {
            if (!string.IsNullOrEmpty(key))
            {
                int splitIndex = key.LastIndexOf('.');
                string typeName = key.Substring(0, splitIndex);

                methodName = key.Substring(splitIndex + 1, key.Length - (splitIndex + 1));
                
                if (string.IsNullOrEmpty(typeName) || string.IsNullOrEmpty(methodName))
                    throw new Exception("Unable to parse callback method: " + key);

                targetBehaviour = null;

                if (targetEventmanager == null)
                    throw new Exception("No target set for key " + key);

                foreach (var behaviour in targetEventmanager.GetComponents<Behaviour>())
                {
                    var n_s = behaviour.GetType().ToString();
                    string[] splitArray = n_s.Split(char.Parse("."));
                    
                    if (typeName == splitArray[splitArray.Length - 1])
                    {
                        targetBehaviour = behaviour;
                        break;
                    }
                }

                if (targetBehaviour == null)
                    throw new Exception("Unable to find target behaviour: key " + key + " typename " + typeName);
            }
        }

    }
}
