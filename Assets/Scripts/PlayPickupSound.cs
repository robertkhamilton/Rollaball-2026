using UnityEngine;

public class PlayPickupSound : MonoBehaviour
{
    public AudioClip pickUpSound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(pickUpSound, transform.position);
        }
    }

    public void playSoundOnThisObject()
    {
        AudioSource.PlayClipAtPoint(pickUpSound, transform.position);
    }

}
