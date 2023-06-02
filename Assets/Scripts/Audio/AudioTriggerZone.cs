using System.Collections;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// This class is responsible for detecting the user within a volume using trigger colliders, 
    /// and playing a looped audio clip with 3D blend until the user exits or is otherwise out of range.
    /// </summary>
    public class AudioTriggerZone : MonoBehaviour
    {
        public bool isPlaying = false;
        public bool withinVolume = false;
        public float userDistance = 0f;

        [Header("Configure")]
        [SerializeField] float fadeRate = 1f;
        [SerializeField] float radius = 5f;

        [Header("Reference")]
        [SerializeField] AudioSource audioSource = null;        // Plays an audio clip 
        [SerializeField] AudioClip audioClip = null;            // Play this clip on enter/stay
        [SerializeField] SphereCollider sphereCollider = null;  // Collider used for detection

        // Cache
        private Coroutine fadeRoutine = null;                   // Coroutine for fading out audio
        public static AudioTriggerZone currentZone = null;      //The current AudioTriggerZone playing at the moment.


        internal virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        private void Awake()
        {
            if(audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }
            if (sphereCollider == null)
            {
                sphereCollider = GetComponent<SphereCollider>();
            }

            if (audioSource != null && audioClip != null)
            {
                audioSource.clip = audioClip;
            }
            if (sphereCollider != null)
            {
                sphereCollider.radius = radius;
            }
        }

        // this update method should only be responsible for triggering out-of-range behaviour
        public virtual void Update()
        {
            userDistance = GetDistance();
            if (PlayerController.Instance != null && isPlaying && (IsOutOfRange() || !withinVolume))// user is not within volume but audio zone is still flagged to play
            {
                // fade out audio if user is out of range
                //Debug.Log("Out of range" + name + " " + audioClip.name);
                FadeOutAudio();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player")/* && !audioSource.isPlaying*/)
            {
                //Debug.Log(name + " Enter");
                if (!withinVolume)
                {
                    withinVolume = true;
                    PlayAudio();
                }
                else if (currentZone != this)
                {
                    PlayAudio();
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player")/* && !audioSource.isPlaying*/)
            {
                if(!withinVolume)
                {
                    withinVolume = true;
                    PlayAudio();
                }
                else if(currentZone != this && isPlaying)
                {
                    FadeOutAudio();
                }
            }
        
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && withinVolume)
            {
                withinVolume = false;
                //Debug.Log(name + " Exit");
                FadeOutAudio();
            }
        }


        /*
     *  PUBLIC METHODS
     */


        // allow the user to play the audio clip once via interaction
        public void PlayAudioOnce()
        {
            PlayAudio(false);
        }

        // Toggle Audio on and off
        public void ToggleAudio()
        {
            if (audioSource.isPlaying)
            {
                StopAudio();
            }
            else
            {
                PlayAudio();
            }
        }


        /*
     *  INTERNAL METHODS
     */


        // Main method to assign clip and play audio
        internal void PlayAudio(bool looping = true)
        {
            if(currentZone != null && currentZone != this)
            {
                //Debug.Log(name + " want to fade out zone: " + currentZone.name);
                currentZone.FadeOutAudio();
            }
            currentZone = this;

            if(audioSource != null)
            {
                audioSource.loop = looping;
                audioSource.clip = GetClip();
                audioSource.volume = 1f;
                audioSource.Play();
                isPlaying = true;
            }
        }

        // Stop Playing Audio
        internal virtual void StopAudio()
        {
            audioSource.Stop();
            isPlaying = false;
            audioSource.loop = false;
            if (currentZone == this)
            {
                currentZone = null;
            }
        }

        internal void FadeOutAudio()
        {
            //Debug.Log("Fade out");
            //stopping = true;

            if(fadeRoutine != null)
            {
                StopCoroutine(fadeRoutine);
            }
            fadeRoutine = StartCoroutine(FadeAudio());
        }

        internal IEnumerator FadeAudio()
        {
            while(audioSource.volume > 0f)
            {
                audioSource.volume -= Time.deltaTime * fadeRate;
                yield return null;
            }
            audioSource.volume = 0f;
            StopAudio();
            fadeRoutine = null;
        }

        internal virtual AudioClip GetClip()
        {
            return audioClip;
        }

        internal bool IsOutOfRange()
        {
            return GetDistance() > audioSource.minDistance + 5f;
        }

        internal float GetDistance()
        {
            //Debug.Log("get distance??");
            //add
            if(PlayerController.Instance == null)
            {
                return float.PositiveInfinity;
            }

            //
            return (PlayerController.Instance.transform.position - transform.position).magnitude;
        }
    }
}



