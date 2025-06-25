using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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

    public search FindComponnetInChild<search>() where search : class
    {
        search _Component = null;


        _Component = this.gameObject.transform.GetComponentInChildren<search>();

        return _Component;


    }
}
