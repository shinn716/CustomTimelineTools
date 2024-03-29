using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Shinn.Timelinie
{
    public class CustomEventPlayable : PlayableAsset
    {
        [SerializeField] public ExposedReference<GameObject> target;
        [SerializeField] public CustomEventClip.ParameterType type = CustomEventClip.ParameterType.NULL;

        [SerializeField] private int IntInput = 0;
        [SerializeField] private float FloatInput = 0;
        [SerializeField] private string StringInput = string.Empty;

        public int selected { get; set; } = 0;
        public List<string> MethodList { get; set; }
        public string Method { get; set; } = string.Empty;

        
        private List<string> eventHandlerListStart = new List<string> { };

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<CustomEventClip>.Create(graph);
            var clip = playable.GetBehaviour();
            clip.target = target.Resolve(graph.GetResolver());

            if (type == CustomEventClip.ParameterType.NULL)
            {
                MethodList = new List<string>();
                return playable;
            }
            else
            {
                MethodList = FindPublicMethod(clip.target).Length == 0 ? new List<string>() : FindPublicMethod(clip.target).ToList();
                SetParameter(clip, type, Method);
                return playable;
            }
        }

        private void SetParameter(CustomEventClip _clip, CustomEventClip.ParameterType _type, string _method)
        {
            switch (_type)
            {
                case CustomEventClip.ParameterType.NULL:
                    break;
                case CustomEventClip.ParameterType.VOID:
                    _clip.Init(type, Method, "");
                    break;
                case CustomEventClip.ParameterType.INT:
                    _clip.Init(type, Method, IntInput);
                    break;
                case CustomEventClip.ParameterType.FLOAT:
                    _clip.Init(type, Method, FloatInput);
                    break;
                case CustomEventClip.ParameterType.STRING:
                    _clip.Init(type, Method, StringInput);
                    break;
            }
        }
        private string[] FindPublicMethod(GameObject _go)
        {
            var _methods = _go.GetComponents<Behaviour>();
            var allMethods = _methods.SelectMany(
                    x => x.GetType()
                        .GetMethods(BindingFlags.Public | BindingFlags.Instance))
                .Where(
                    x =>
                    {
                        if (type == CustomEventClip.ParameterType.INT)
                        {
                            return (x.ReturnType == typeof(void)) && (x.GetParameters().Length == 1) &&
                                   (x.GetParameters()[0].ParameterType == typeof(int));
                        }
                        else if (type == CustomEventClip.ParameterType.FLOAT)
                        {
                            return (x.ReturnType == typeof(void)) && (x.GetParameters().Length == 1) &&
                                   (x.GetParameters()[0].ParameterType == typeof(float));
                        }
                        else if (type == CustomEventClip.ParameterType.STRING)
                        {
                            return (x.ReturnType == typeof(void)) && (x.GetParameters().Length == 1) &&
                                   (x.GetParameters()[0].ParameterType == typeof(string));
                        }
                        else
                        {
                            return (x.ReturnType == typeof(void)) && (x.GetParameters().Length == 0);
                        }
                    }).ToArray();

            var callbackMethodsEnumarable = allMethods.Select(
            x => x.DeclaringType.ToString() + "." + x.Name);

            string[] callbackMethods = eventHandlerListStart.Concat(callbackMethodsEnumarable).ToArray();
            var lastTwoDotPattern = @"[^\.]+\.[^\.]+$";

            var callbackMethodNames = callbackMethods.Select(m =>
            {
                var result = Regex.Match(m, lastTwoDotPattern, RegexOptions.RightToLeft);
                return result.Success ? result.Value : m;
            }).ToArray();


            List<string> list = new List<string>();
            for (int i = 0; i < callbackMethodNames.Length; i++)
            {
                string[] sArray = callbackMethodNames[i].Split('.');
                if (!(sArray[0].Contains("MonoBehaviour") ||
                      sArray[0].Contains("Component") ||
                      sArray[0].Contains("Object") ||
                      sArray[0].Contains("StandaloneInputModule")))
                    list.Add(callbackMethodNames[i]);
            }

            return list.ToArray();
        }
    }
}