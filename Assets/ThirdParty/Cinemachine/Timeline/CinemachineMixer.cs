using UnityEngine;
using UnityEngine.Playables;

namespace Cinemachine.Timeline
{
    public sealed class CinemachineMixer : PlayableBehaviour
    {
        // The brain that this track controls
        private CinemachineBrain mBrain;
        private int mBrainOverrideId = -1;

        public override void OnPlayableDestroy(Playable playable)
        {
            if (mBrain != null)
                mBrain.ReleaseCameraOverride(mBrainOverrideId); // clean up
            mBrainOverrideId = -1;
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            base.ProcessFrame(playable, info, playerData);

            // Get the brain that this track controls.
            // Older versions of timeline sent the gameObject by mistake.
            GameObject go = playerData as GameObject;
            if (go == null)
                mBrain = (CinemachineBrain)playerData;
            else
                mBrain = go.GetComponent<CinemachineBrain>();
            if (mBrain == null)
                return;

            // Find which clips are active.  We can process a maximum of 2.
            // In the case that the weights don't add up to 1, the outgoing weight
            // will be calculated as the inverse of the incoming weight.
            int activeInputs = 0;
            ICinemachineCamera camA = null;
            ICinemachineCamera camB = null;
            float camWeight = 1f;
            for (int i = 0; i < playable.GetInputCount(); ++i)
            {
                CinemachineShotPlayable cam
                    = ((ScriptPlayable<CinemachineShotPlayable>)playable.GetInput(i)).GetBehaviour();
                float weight = playable.GetInputWeight(i);
                if (cam != null && cam.VirtualCamera != null
                    && playable.GetPlayState() == PlayState.Playing
                    && weight > 0.0001f)
                {
                    if (activeInputs == 1)
                        camB = camA;
                    camWeight = weight;
                    camA = cam.VirtualCamera;
                    ++activeInputs;
                    if (activeInputs == 2)
                        break;
                }
            }

            // Override the Cinemachine brain with our results.
            // This is a simulation, so we need a fixed delta time.
            float deltaTime = Time.fixedDeltaTime;
            if (info.evaluationType != FrameData.EvaluationType.Playback)
                deltaTime = 0;
            mBrainOverrideId = mBrain.SetCameraOverride(
                    mBrainOverrideId, camB, camA, camWeight, deltaTime);
        }
    }
}
