using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractionObject : MonoBehaviour
{

    public InteractionType interactionType;
    public static Dictionary<InteractionObject, InteractionType> interactionDictionary = new Dictionary<InteractionObject, InteractionType>();

    private void OnEnable()
    {
        interactionDictionary.Add(this, interactionType);
    }

    private void OnDisable()
    {
        interactionDictionary.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public enum InteractionType
{
    None,
    DanceSpot,
    Dialogue,
    Quest
};