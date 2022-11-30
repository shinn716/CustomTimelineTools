using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RenderHeads.Media.AVProVideo;

namespace Shinn.Timelinie
{
    public class CustomAvproMediaPlayable : PlayableAsset
    {
        [SerializeField] ExposedReference<MediaPlayer> mediaPlayer;
        [SerializeField] MediaPlayer.FileLocation fileLocation;
        [SerializeField] string path;
        [SerializeField] bool loop = false;
        [SerializeField] float playbackRate = 1;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<CustomAvproMediaClip>.Create(graph);
            var selectClip = playable.GetBehaviour();

            selectClip.mediaPlayer = mediaPlayer.Resolve(graph.GetResolver());
            selectClip.fileLocation = fileLocation;
            selectClip.playbackRate = playbackRate; 
            selectClip.path = path;
            selectClip.loop = loop;
            return playable;
        }

        [ContextMenu("Test")]
        public void Test()
        {

        }
    }
}