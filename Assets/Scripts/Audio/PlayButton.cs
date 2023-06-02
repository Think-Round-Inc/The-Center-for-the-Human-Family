using UnityEngine;

// this class holds a reference to an AudioTriggerZone object, whose members contain an AudioSource and clip references
namespace Audio
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] AudioTriggerZone audioZone = null;

        private void OnMouseDown()
        {
            if (PlayerController.Instance != null && Vector3.Distance(PlayerController.Instance.transform.position, transform.position) < 10f)
            {
                if(audioZone != null)
                {
                    audioZone.PlayAudioOnce();
                }
            }
        }
    }
}
