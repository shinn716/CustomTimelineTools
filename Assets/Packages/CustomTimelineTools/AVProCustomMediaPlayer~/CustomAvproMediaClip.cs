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
        public string mediaUrl { get; set; }
        public bool loop { get; set; } = false;

        private bool startOnce = false;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (mediaPlayer != null)
            {
                if (startOnce)
                    return;
                startOnce = true;

                mediaPlayer.OpenVideoFromFile(fileLocation, mediaUrl, loop);
                mediaPlayer.Play();
            }
        }

        // On clip end
        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (!Application.isPlaying)
            {
                return;
            }

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