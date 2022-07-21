using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Shinn.Timelinie
{
    public class CustomMsgClip : PlayableBehaviour
    {
        public GameObject targetEventmanager { get; set; }
        public CustomMsgPlayable.ParameterType type { get; set; }
        public object input { get; set; }
        public bool useClipDuring { get; set; }

        private bool startOnce = false;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (targetEventmanager == null)
                return;

            if (startOnce)
                return;

            startOnce = true;
            
            if (useClipDuring)
            {
                var value = (float) playable.GetDuration();
                input = value;
            }

            object[] inputObjs = { type, input };
            targetEventmanager.SendMessage("StartEvent", inputObjs);
        }

        // On clip end
        //public override void OnBehaviourPause(Playable playable, FrameData info)
        //{
        //    if (!onEndTrigger)
        //        return;

        //    if (input == null)
        //        return;

        //    var duration = playable.GetDuration();
        //    var count = playable.GetTime() + info.deltaTime;

        //    if (info.effectivePlayState == PlayState.Paused && count > duration)         // timeline done: playable.GetGraph().GetRootPlayable(0).IsDone()
        //    {
        //        object[] inputObjs = { type, input };
        //        targetEventmanager.SendMessage("EndEvent", inputObjs);
        //    }
        //}
    }
}
