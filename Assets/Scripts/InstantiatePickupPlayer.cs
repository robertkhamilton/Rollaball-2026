using UnityEngine;

public class InstantiatePickupPlayer : MonoBehaviour
{
    public AudioSource PlayClipAtPointNoDoppler(AudioClip clip, Vector3 position, float volume = 1f)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = position;

        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.dopplerLevel = 0f; // Turn off Doppler here
        audioSource.spatialBlend = 1f; // Ensure it's 3D sound
        audioSource.Play();

        Destroy(tempGO, clip.length);
        return audioSource;
    }
}
