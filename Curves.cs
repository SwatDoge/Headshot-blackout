using UnityEngine;

namespace HeadshotBlackout.curves
{
    internal class Curves
    {
        public static AnimationCurve deathCurve = new AnimationCurve(new Keyframe[] {
            new Keyframe(0f, 0f),
            new Keyframe(0.05f, 3f),
            new Keyframe(5.00f, 3f)
        });

        public static AnimationCurve deathAudioCurve = new AnimationCurve(new Keyframe[] {
            new Keyframe(0f, 1f),
            new Keyframe(0.1f, 1f),
            new Keyframe(0.125f, 0f),
            new Keyframe(4.50f, 0f),
            new Keyframe(5.00f, 1f)
        });
    }
}
