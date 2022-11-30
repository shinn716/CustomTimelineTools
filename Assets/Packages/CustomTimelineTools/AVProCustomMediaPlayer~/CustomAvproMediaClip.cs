using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RenderHeads.Media.AVProVideo;

namespace Shinn.Timelinie
{
    public class CustomAvproMediaClip : PlayableBehaviour
    {
        public MediaPlayer mediaPlayer { get; set; }
        public MediaPlayer.FileLocation fileLocation { get; set; }
        public string path { get; set; }
        public bool loop { get; set; } = false;
        public float playbackRate { get; set; } = 1f;


        private bool startOnce = false;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (!Application.isPlaying)
                return;

            if (mediaPlayer != null)
            {
                if (startOnce)
                    return;

                startOnce = true;

                mediaPlayer.OpenVideoFromFile(fileLocation, path);
                mediaPlayer.Control.SetLooping(loop);
                mediaPlayer.Control.SetPlaybackRate(playbackRate);
                mediaPlayer.Control.Play();
            }
        }

        // On clip end
        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (!Application.isPlaying)
                return;

            var duration = playable.GetDuration();
            var time = playable.GetTime();
            var count = time + info.deltaTime;

            if ((info.effectivePlayState == PlayState.Paused && count > duration) || Mathf.Approximately((float)time, (float)duration))
            {
                mediaPlayer.Stop();
            }
        }
    }
}