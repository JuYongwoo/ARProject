using UnityEngine;
using UnityEngine.Android;


public class SceneManager : MonoBehaviour
{
    public enum Sounds
    {
        spawn,
        destroy
    }
    private AudioSource audioSource;
    private AudioClip bgmClip;
    private AudioClip spawnaudio;
    private AudioClip destroyaudio;

    private void Awake()
    {
        EnemyBase.playaudio = PlaySFX;
        spawnaudio = Resources.Load<AudioClip>("Spawn");
        destroyaudio = Resources.Load<AudioClip>("Destroy");
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

        AddOrGetComponent<AudioListener>(gameObject);
        audioSource = AddOrGetComponent<AudioSource>(gameObject);
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        bgmClip = Resources.Load<AudioClip>("BGM");
        audioSource.clip = bgmClip;
        audioSource.Play();

    }

    private void PlaySFX(Sounds sd)
    {
        if(sd == Sounds.spawn)
        {
            audioSource.PlayOneShot(spawnaudio);

        }
        else if(sd == Sounds.destroy)
        {

            audioSource.PlayOneShot(destroyaudio);
        }
    }

    public T AddOrGetComponent<T>(GameObject obj) where T : Component
    {
        T comp = obj.GetComponent<T>();
        if (comp == null)
            comp = obj.AddComponent<T>();
        return comp;
    }

}
