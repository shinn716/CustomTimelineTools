using Shinn.Timelinie;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(85 / 255f, 255 / 255f, 85 / 255f)]
[TrackClipType(typeof(CustomTextPlayable))]
//[TrackBindingType(typeof(Text))]
public class CustomTextTracker : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<CustomTextClip>.Create(graph, inputCount);
    }
}