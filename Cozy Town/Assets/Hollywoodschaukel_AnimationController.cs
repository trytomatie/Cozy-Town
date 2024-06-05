using UnityEngine;

public class Hollywoodschaukel_AnimationController : MonoBehaviour
{
    public InteractionObject[] interactionObject;
    public Animator anim;
    // Update is called once per frame
    void FixedUpdate()
    {
        bool isOccupied = false;
        foreach(InteractionObject obj in interactionObject)
        {
            if(obj.interacting)
            {
                isOccupied = true;
                break;
            }
        }
        if(isOccupied)
        {
            foreach(InteractionObject obj in interactionObject)
            {
                anim.SetBool("Occupied", true);
            }
        }
        else
        {
            foreach(InteractionObject obj in interactionObject)
            {
                anim.SetBool("Occupied", false);
            }
        }   
    }
}
