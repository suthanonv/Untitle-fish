using JetBrains.Annotations;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioManager instance;
    public AudioManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

}
