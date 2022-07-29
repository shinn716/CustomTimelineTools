using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Playables;

namespace Shinn.Timelinie
{
    public class CustomEventClip : PlayableBehaviour
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
            NULL,
            VOID,
            INT,
            FLOAT,
            STRING,
        }

        public GameObject target { get; set; } = null;

        private ParameterType type = ParameterType.NULL;
        private string key = null;
        private object input = null;
        private MethodData methodData;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            target = playerData as GameObject;
        }

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            base.OnBehaviourPlay(playable, info);
            if (target != null)
                Play(type, key, input);
        }

        public void Init(ParameterType _type, string _key, object _input)
        {
            type = _type;
            key = _key;
            input = _input;
        }

        private void Play(ParameterType _type, string _key, object _input)
        {
            if (target == null)
                return;

            if (_key == null || _key == string.Empty)
                return;

            methodData = GetInvocationInfo(_type, _key);
            //Debug.Log(_key + " " + _input);

            try
            {
                switch (_type)
                {
                    default:
                        break;
                    case ParameterType.VOID:
                        methodData.methodInfo.Invoke(methodData.behaviour, null);
                        break;
                    case ParameterType.INT:
                        int.TryParse(_input.ToString(), out int iresult);
                        methodData.methodInfo.Invoke(methodData.behaviour, new object[] { iresult });
                        break;
                    case ParameterType.FLOAT:
                        float.TryParse(_input.ToString(), out float fresult);
                        methodData.methodInfo.Invoke(methodData.behaviour, new object[] { fresult });
                        break;
                    case ParameterType.STRING:
                        methodData.methodInfo.Invoke(methodData.behaviour, new object[] { _input });
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.Log("Wrong type:" + _input + " " + e);
            }
        }


        private MethodData GetInvocationInfo(ParameterType _type, string _key)
        {
            Behaviour targetBehaviour = null;
            string methodName = null;
            GetBehaviourAndMethod(_key, ref targetBehaviour, ref methodName);

            if (targetBehaviour != null)
            {
                bool withParameter = _type == ParameterType.VOID ? false : true;

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


        private void GetBehaviourAndMethod(string key, ref Behaviour targetBehaviour, ref string methodName)
        {
            if (!string.IsNullOrEmpty(key))
            {
                int splitIndex = key.LastIndexOf('.');
                string typeName = key.Substring(0, splitIndex);

                methodName = key.Substring(splitIndex + 1, key.Length - (splitIndex + 1));

                if (string.IsNullOrEmpty(typeName) || string.IsNullOrEmpty(methodName))
                    throw new Exception("Unable to parse callback method: " + key);

                targetBehaviour = null;

                if (target == null)
                    throw new Exception("No target set for key " + key);

                foreach (var behaviour in target.GetComponents<Behaviour>())
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