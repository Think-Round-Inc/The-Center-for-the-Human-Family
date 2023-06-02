using System.Collections;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// This class is responsible for playing audio clips dependant on user's position. 
    /// If the user enters the inner radius, the user should hear the innerClip. 
    /// If the user exits the inner radius, the base audioClip should play.
    /// </summary>
    public class AudioTriggerZoneNested : AudioTriggerZone
    {
        [Header("Nested Audio Zone")]
        [SerializeField] AudioClip innerClip = null; // Play this clip when the user has entered the "inner" area.
        [SerializeField] float innerRadius = 3.5f;

        // Cache
        private bool innerPlaying = false;
        private bool switching = false;
        Coroutine switchRoutine = null;


        internal override void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, innerRadius);
            base.OnDrawGizmos();
        }

        //Depending on distance, switch between appropriate clips
        public override void Update()
        {
            if (withinVolume) //only check if within volume
            {
                if (GetDistance() < innerRadius && !innerPlaying)
                {
                    //stop playing the outer clip
                    //play the inner clip

                    if (!switching)
                    {
                        switching = true;
                        if (switchRoutine != null)
                        {
                            StopCoroutine(switchRoutine);
                        }

                        switchRoutine = StartCoroutine(SwitchClips());
                        innerPlaying = true;

                    }
                }
                else if (GetDistance() > innerRadius && innerPlaying)
                {
                    //stop playing inner clip
                    //play outer clip
                    if (!switching)
                    {
                        switching = true;
                        if (switchRoutine != null)
                        {
                            StopCoroutine(switchRoutine);
                        }

                        switchRoutine = StartCoroutine(SwitchClips());
                        innerPlaying = false;
                    }
                }
            }
            base.Update();
        }

        //if the object is a nested audio zone, check user distance to acquire appropriate clip
        internal override AudioClip GetClip()
        {
            if (GetDistance() <= innerRadius)
            {
                return innerClip;
            }
            else return base.GetClip();
        }

        // Fade one clip out before playing another
        private IEnumerator SwitchClips()
        {
            yield return FadeAudio(); //wait until audio fades out
            PlayAudio();
            switchRoutine = null;
            switching = false;
        }
    }
}
