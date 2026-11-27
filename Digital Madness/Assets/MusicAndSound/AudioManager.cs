using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    //public AudioSource sfxSource;

    public AudioClip menuBackgroundMusic;
    //public AudioClip shootSound;
    //public AudioClip explosionSound;

    void Start()
    {
        musicSource.clip = menuBackgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    //public void PlayShoot()
    //{
    //    sfxSource.PlayOneShot(shootSound);
    //}

    //public void PlayExplosion()
    //{
    //    sfxSource.PlayOneShot(explosionSound);
    //}
}
