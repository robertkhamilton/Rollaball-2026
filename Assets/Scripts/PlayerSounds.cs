using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSounds : MonoBehaviour
{
    public AudioClip pickUpSound;

    private AudioSource playerAudioSource;

    private void Start()
    {
        // get a reference to the AudioSource on this player object
        playerAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            // Works but is monophonic and PlayClipAtPoint has audible artifacts due to Doppler
            //AudioSource.PlayClipAtPoint(pickUpSound, transform.position);

            // Could also call the function on the object rather than on the player
            //other.GetComponent<PlayPickupSound>().playSoundOnThisObject();
        }
    }

    // Can call this function from the PlayerController.cs script to assure that it's triggered before SetActive(false)
    public void PlaySoundFromPlayerControllerScript()
    {
        // PlayClipAtPoint here has the same Doppler artifacts
        //AudioSource.PlayClipAtPoint(pickUpSound, transform.position);

        // Trigger sound on AudioSource attached to Player to make it trigger on the player (if using 3D) or in 2D, thus avoiding Doppler
        playerAudioSource.clip = pickUpSound;       // set our AudioClip here in code rather than directly in the editor
        playerAudioSource.dopplerLevel = 0f;        // turn off Doppler if we want to
        playerAudioSource.spatialBlend = 1f;        // makes this a 3D sound
        // playerAudioSource.spatialBlend = 0f;     // makes this a 2D sound
        playerAudioSource.Play();
 
    }

}
