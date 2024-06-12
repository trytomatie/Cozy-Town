using CrashKonijn.Goap.Interfaces;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class InteractionObject : MonoBehaviour, IInteractable
{
    public AnimationType animationType = AnimationType.None;
    public EquipedItem equipedItem = EquipedItem.None;
    public GameObject occupant = null;
    public bool interacting = false;
    public float duration = 15;



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

    public GameObject Occupant 
    { 
        get => occupant;
        set { 
            occupant = value;
            if(occupant == null)
            {
                interacting = false;
            }
        }
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
    Sit,
    Paint,
    Roasting,
    Watering,
    GroundSit,
    Eeping,
}

public enum EquipedItem
{
    None,
    Stick,
    WateringCan,
    PaintBrush
}