using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using RenderHeads.Media.AVProVideo;

namespace Shinn.Timelinie
{
    public class CustomAvproMediaPlayable : PlayableAsset
    {
        public ExposedReference<MediaPlayer> mediaPlayer;
        public MediaPlayer.FileLocation fileLocation;
        public string mediaUrl;
        public bool loop = false;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<CustomAvproMediaClip>.Create(graph);
            var selectClip = playable.GetBehaviour();

            selectClip.mediaPlayer = mediaPlayer.Resolve(graph.GetResolver());
            selectClip.mediaUrl = mediaUrl;
            selectClip.fileLocation = fileLocation;
            selectClip.loop = loop;
            return playable;
        }
    }
}