using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource moveSound;
    [SerializeField] private AudioSource pickupSound;
    [SerializeField] private AudioSource dropSound;
    [SerializeField] private AudioSource bgmSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBGM();
    }

    public void PlayMoveSound()
    {
        moveSound.Play();
    }

    public void PlayPickupSound()
    {
        pickupSound.Play();
    }

    public void PlayDropSound()
    {
        dropSound.Play();
    }

    public void PlayBGM()
    {
        if (bgmSound != null && !bgmSound.isPlaying)
        {
            bgmSound.Play();
        }
    }

    public void StopBGM()
    {
        if (bgmSound != null && bgmSound.isPlaying)
        {
            bgmSound.Stop();
        }
    }
}
