using System;
using UnityEngine;
using UnityEngine.Events;

public class PhaseListener : MonoBehaviour
{
    public event Action OnActiveEvent;
    public event Action OnUnActiveEvent;
    [SerializeField] Phase activePhase;
    [SerializeField] UnityEvent OnActive, OnUnActive;


    private void Awake()
    {
        FindAnyObjectByType<PhaseController>().PhaseUpdateEvent += UpdatePhase;
    }

    void UpdatePhase(Phase phase)
    {
        if (phase != activePhase)
        {
            OnUnActiveEvent?.Invoke();
            OnActive?.Invoke();
            this.gameObject.SetActive(false);
        }
        else
        {
            OnActiveEvent?.Invoke();
            OnUnActive?.Invoke();
            this.gameObject.SetActive(true);
        }
    }
}
