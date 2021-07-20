
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Shinn.Timelinie
{
    [TrackColor(0.8f, 0.66f, .7f)]
    [TrackClipType(typeof(CustomEventClip))]
    [TrackBindingType(typeof(GameObject))]
    public class CustomEventTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var director = go.GetComponent<PlayableDirector>();
            var trackTargetObject = director.GetGenericBinding(this) as GameObject;

            foreach (var clip in GetClips())
            {
                var playableAsset = clip.asset as CustomEventClip;

                if (playableAsset)
                {
                    if (trackTargetObject)
                    {
                        playableAsset.template.targetEventmanager = trackTargetObject;
                    }
                }
            }

            var scriptPlayable = ScriptPlayable<CustomEventMixerBehavior>.Create(graph, inputCount);
            return scriptPlayable;
        }
    }
}