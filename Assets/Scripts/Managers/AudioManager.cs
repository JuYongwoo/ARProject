using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public enum Sounds
{
    Spawn,
    Destroy,
    BGM
}

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    private Dictionary<Sounds, AudioClip> soundClips;
    private void Awake()
    {
        EnemyBase.playaudio = PlaySFX;
        soundClips = Util.mapDictionaryWithEnumAndLoad<Sounds, AudioClip>("Audios");
    }
    void Start()
    {
        // Android 권한 요청
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
#endif

        Util.AddOrGetComponent<AudioListener>(gameObject);
        audioSource = Util.AddOrGetComponent<AudioSource>(gameObject);
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        audioSource.clip = soundClips[Sounds.BGM];
        audioSource.Play();

    }

    private void PlaySFX(Sounds sd)
    {
        audioSource.PlayOneShot(soundClips[sd]);
    }


}
