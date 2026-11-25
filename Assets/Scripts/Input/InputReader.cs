using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{

    [SerializeField] protected bool _mustBeWatched;
    [SerializeField] protected GameObject _watchedObject;
    protected Action _afterUpdate;

    void Update()
    {
        ProcessCurrentPressedKeys();
    }

    protected virtual void ProcessCurrentPressedKeys() { }

    public void SetAfterUpdateEvent(Action action) => _afterUpdate = action;
}
