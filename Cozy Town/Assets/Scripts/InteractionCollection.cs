using NUnit.Framework;
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
        print($"Interactables:{Interactables.Count()} DanceSpots: {Get<DanceSpot_InteractionObject>().Count()}");
    }

    public T[] Get<T>()
    where T : IInteractable
    {
        return this.Interactables.Where(x => x is T).Cast<T>().ToArray();
    }

}
