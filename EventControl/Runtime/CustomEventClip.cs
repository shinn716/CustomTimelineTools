using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;
using System;

namespace Shinn.Timelinie
{
    public class CustomEventClip : PlayableBehaviour
    {
        public GameObject targetEventmanager { get; set; }
        public CustomEventControlPlayable.ParameterType type { get; set; }
        public object input { get; set; }
        public bool onStartTrigger { get; set; }
        public bool onEndTrigger { get; set; }

        private bool startOnce = false;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (!onStartTrigger)
                return;

            if (input == null)
                return;

            if (targetEventmanager != null && !startOnce)
            {
                startOnce = true;
                object[] inputObjs = { type, input };
                targetEventmanager.SendMessage("StartEvent", inputObjs);
            }
        }

        // On clip end
        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (!onEndTrigger)
                return;

            if (input == null)
                return;

            var duration = playable.GetDuration();
            var count = playable.GetTime() + info.deltaTime;

            if (info.effectivePlayState == PlayState.Paused && count > duration)         // timeline done: playable.GetGraph().GetRootPlayable(0).IsDone()
            {
                object[] inputObjs = { type, input };
                targetEventmanager.SendMessage("EndEvent", inputObjs);
            }
        }
    }
}
