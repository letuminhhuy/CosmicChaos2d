using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioSource gamePlayAudioSource;
    [SerializeField] private AudioClip hiting;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip collect;
    [SerializeField] private AudioClip gameWin;
    [SerializeField] private AudioClip gameOver;

    public void gamePlayAudio()
    {
        gamePlayAudioSource.Play();
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
        gamePlayAudioSource.Stop();
        m_AudioSource.PlayOneShot(death);
    }
    public void GameWinSound()
    {
        gamePlayAudioSource.Stop();
        m_AudioSource.PlayOneShot(gameWin);
    }
    public void GameOverSound()
    {
        gamePlayAudioSource.Stop();
        m_AudioSource.PlayOneShot(gameOver);
    }
}
