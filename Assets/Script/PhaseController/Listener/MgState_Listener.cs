using System;
using UnityEngine;
using UnityEngine.Events;

public class MgState_Listener : MonoBehaviour
{
    [SerializeField] UnityEvent<bool> OnMinigameUpdate;
    public event Action<bool> OnMingameStateUpdate;
    private void Awake()
    {
        FindAnyObjectByType<PhaseController>().MiniGameStateUpdateEvent += OnMiniGameWinStateUpdate;

    }
    void OnMiniGameWinStateUpdate(bool isWin)
    {
        OnMinigameUpdate?.Invoke(isWin);
        OnMingameStateUpdate?.Invoke(isWin);
    }
}
