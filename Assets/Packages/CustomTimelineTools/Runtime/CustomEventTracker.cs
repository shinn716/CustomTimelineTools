using Shinn.Timelinie;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0 / 255f, 92 / 255f, 240 / 255f)]
[TrackClipType(typeof(CustomEventPlayable))]
public class CustomEventTracker : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<CustomEventClip>.Create(graph, inputCount);
    }
}