using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace Shinn.Timelinie
{
    public class CustomEventControlPlayable : PlayableAsset
    {
        public enum ParameterType
        {
            Int,
            Float,
            String
        }

        public ExposedReference<GameObject> targetEventManager;

        public ParameterType parameterType = ParameterType.Int;
        public string input;
        public bool onStartTrigger = true;
        public bool onEndTrigger = false;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<CustomEventClip>.Create(graph);

            var selectClip = playable.GetBehaviour();

            selectClip.targetEventmanager = targetEventManager.Resolve(graph.GetResolver());
            selectClip.input = input;
            selectClip.type = parameterType;
            selectClip.onStartTrigger = onStartTrigger;
            selectClip.onEndTrigger = onEndTrigger;
            return playable;
        }
    }
}
