using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioSource defaultAudioSource;
    [SerializeField] private AudioClip hiting;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip collect;
    [SerializeField] private AudioClip gameWin;
    [SerializeField] private AudioClip gameOver;

    public void gamePlayAudio()
    {
        defaultAudioSource.Play();
    }

    public void HitSound()
    {
        m_AudioSource.PlayOneShot(hiting);
    }
    public void CollectSound()
    {
        m_AudioSource.PlayOneShot(collect);
    }
    public void DeathSound()
    {
        defaultAudioSource.Stop();
        m_AudioSource.PlayOneShot(death);
    }
    public void GameWinSound()
    {
        defaultAudioSource.Stop();
        m_AudioSource.PlayOneShot(gameWin);
    }
    public void GameOverSound()
    {
        defaultAudioSource.Stop();
        m_AudioSource.PlayOneShot(gameOver);
    }
}
