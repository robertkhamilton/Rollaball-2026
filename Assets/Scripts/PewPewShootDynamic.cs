using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class PewPewShootDynamic : MonoBehaviour
{
    [Header("Input Setup")]
    [SerializeField] private InputActionReference shootActionReference;

    [Header("Audio Setup")]
    [SerializeField] private AudioClip[] shootSounds;

    [Header("Randomization Modifiers")]
    [Range(0f, 0.3f)] [SerializeField] private float pitchVariation = 0.05f;
    [Range(0f, 0.2f)] [SerializeField] private float volumeVariation = 0.05f;

    private AudioSource audioSource;
    private int lastPlayedIndex = -1;
    private float defaultPitch;
    private float defaultVolume;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        // Save the baseline values set on your AudioSource component
        defaultPitch = audioSource.pitch;
        defaultVolume = audioSource.volume;
    }

    private void OnEnable()
    {
        if (shootActionReference != null)
        {
            shootActionReference.action.Enable();
            shootActionReference.action.started += OnShootTriggered;
        }
    }

    private void OnDisable()
    {
        if (shootActionReference != null)
        {
            shootActionReference.action.started -= OnShootTriggered;
        }
    }

    private void OnShootTriggered(InputAction.CallbackContext context)
    {
        if (shootSounds == null || shootSounds.Length == 0 || audioSource == null) return;

        int indexToPlay = GetRandomIndexNoRepeat();
        AudioClip selectedClip = shootSounds[indexToPlay];

        if (selectedClip != null)
        {
            // Apply subtle pitch variations
            audioSource.pitch = defaultPitch + Random.Range(-pitchVariation, pitchVariation);

            // Apply subtle volume variations (ensuring it stays within standard 0.0 to 1.0 bounds)
            float randomVolume = defaultVolume + Random.Range(-volumeVariation, volumeVariation);
            float finalVolume = Mathf.Clamp(randomVolume, 0f, 1f);

            // PlayOneShot honors the AudioSource's pitch property, but we pass finalVolume manually
            audioSource.PlayOneShot(selectedClip, finalVolume);
        }
    }

    private int GetRandomIndexNoRepeat()
    {
        // If there's only 1 sound, repetition cannot be avoided
        if (shootSounds.Length <= 1) return 0;

        int newIndex = lastPlayedIndex;

        // Roll for a new index until it differs from the last one
        while (newIndex == lastPlayedIndex)
        {
            newIndex = Random.Range(0, shootSounds.Length);
        }

        lastPlayedIndex = newIndex;
        return newIndex;
    }
}