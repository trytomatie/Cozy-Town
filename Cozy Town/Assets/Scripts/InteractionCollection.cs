using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class InteractionCollection : MonoBehaviour
{
    [SerializeField]public List<IInteractable> Interactables = new List<IInteractable>();
    public static InteractionCollection Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        print($"Interactables:{Interactables.Count()} DanceSpots: {Get<Fun_InteractionObject>().Count()}");
    }

    public T[] Get<T>()
    where T : IInteractable
    {
        return this.Interactables.Where(x => x is T).Cast<T>().ToArray();
    }

    public static int CountOfAvailableFunSpots()
    {
        return InteractionCollection.Instance.Get<Fun_InteractionObject>().Where(e => e.Occupant == null).Count();
    }

    internal static int CountOfAvailableRestingSpots()
    {
        return InteractionCollection.Instance.Get<RestingPlace_InteractionObject>().Where(e => e.Occupant == null).Count();
    }
}
