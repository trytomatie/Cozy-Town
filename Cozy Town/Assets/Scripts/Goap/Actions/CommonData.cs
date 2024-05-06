using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using UnityEngine.UIElements;

public class CommonData : IActionData
{
    public ITarget Target { get ; set; }
    public TransformTarget TargetTransform { get
        {
            if(Target is TransformTarget)
                return (TransformTarget)Target;
            return null;
        } 
        set => Target = value; }
    public float Timer { get; set; }
    public InteractionObject interactionTarget;
    public bool ActionStarted = false;
}