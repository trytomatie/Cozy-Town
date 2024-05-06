using CrashKonijn.Goap.Interfaces;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractionObject : MonoBehaviour, IInteractable
{
    public AnimationType animationType = AnimationType.None;
    public IMonoAgent occupant = null;
    private void OnEnable()
    { 
        InteractionCollection.Instance.Interactables.Add(this);
    }

    private void OnDisable()
    {
        InteractionCollection.Instance.Interactables.Remove(this);
    }

    public virtual void Interact()
    {

    }
}

[SerializeField]
public interface IInteractable
{
    void Interact();
}

public enum AnimationType
{
    None,
    Dance,
    Sit
}