using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class PewPewShootStatic : MonoBehaviour
{
    [Header("Input Setup")]
    [SerializeField] private InputActionReference shootActionReference;

    [Header("Audio Setup")]
    [SerializeField] private AudioClip shootSound;

    private AudioSource myAudioSource;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // Link the method to the 'started' phase (fires instantly when pressed)
        if (shootActionReference != null)
        {
            shootActionReference.action.Enable();
            shootActionReference.action.started += OnShootTriggered;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks or null reference bugs
        if (shootActionReference != null)
        {
            shootActionReference.action.started -= OnShootTriggered;
        }
    }

    private void OnShootTriggered(InputAction.CallbackContext context)
    {
        if (shootSound != null && myAudioSource != null)
        {
            // PlayOneShot allows sounds to overlap if the player clicks rapidly
            myAudioSource.PlayOneShot(shootSound);
        }
    }

 /*
 * Could do a simple script like this if you don't want to use the "New Input System"
 * - this just makes a direct call to the Mouse instead of setting up an Input
  

    public class QuickShootAudio : MonoBehaviour
    {
        public AudioClip shootSound;
        private AudioSource audioSource;

        void Start() => audioSource = GetComponent<AudioSource>();

        void Update()
        {
            // Polls the hardware directly for a single frame click event
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                audioSource.PlayOneShot(shootSound);
            }
        }
    }
 */
}
