using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource aud;

    private void Awake()
    {
        instance = this;

        aud = GetComponent<AudioSource>();
    }

    /// <summary>
    /// ���񭵮ĨåB�H�����q
    /// </summary>
    /// <param name="sound">�n���񪺭���</param>
    /// <param name="min">�̤p���q</param>
    /// <param name="max">�̤j���q</param>
    public void PlaySoundRandomVolue(AudioClip sound, float min, float max)
    {
        float randomVolume = Random.Range(min, max);
        aud.PlayOneShot(sound, randomVolume);
    }
}
