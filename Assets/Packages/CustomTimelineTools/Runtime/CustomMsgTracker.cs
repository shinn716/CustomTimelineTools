using Shinn.Timelinie;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(15 / 255f, 79 / 255f, 147 / 255f)]
[TrackClipType(typeof(CustomMsgPlayable))]
//[TrackBindingType(typeof(Text))]
public class CustomMsgTracker : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<CustomMsgClip>.Create(graph, inputCount);
    }
}