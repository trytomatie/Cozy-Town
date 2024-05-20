using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Singelton
    public static SoundManager Instance { get; private set; }
    public AudioClip[] sounds;
    public GameObject soundPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void PlaySound(int soundIndex, Vector3 position)
    {
        GameObject sound = Instantiate(Instance.soundPrefab, position, Quaternion.identity);
        sound.GetComponent<AudioSource>().clip = Instance.sounds[soundIndex];
        sound.GetComponent<AudioSource>().Play();
        Destroy(sound, sound.GetComponent<AudioSource>().clip.length);
    }
}
