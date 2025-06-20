using JetBrains.Annotations;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private AudioSource instance;
    public AudioSource Instance
    {
        get
        {
            if (!instance)
            {
                instance = new GameObject().AddComponent<AudioSource>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

}
