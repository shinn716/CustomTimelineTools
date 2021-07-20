using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Shinn.Timelinie
{
    [Serializable]
    public class CustomEventClip : PlayableAsset
    {
        public CustomEventControlPlayable template = new CustomEventControlPlayable();

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<CustomEventControlPlayable>.Create(graph, template);
            return playable;
        }
    }
}
