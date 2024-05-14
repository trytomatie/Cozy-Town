using UnityEngine;

public class FootStepTrigger : MonoBehaviour
{
    private bool footStepRight = false;
    private bool footStepLeft = false;
    public Transform rightFoot;
    public Transform leftFoot;
    public float rightFootoffset = 0.1f;
    public float leftFootoffset = 0.1f;
    public LayerMask floorLayer;
    public ParticleSystem footStep;
    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(rightFoot.position, Vector3.down * rightFootoffset, out hit, rightFootoffset,floorLayer))
        {
            FootStepRight = true;
        }
        else
        {
            FootStepRight = false;
        }
        if (Physics.Raycast(leftFoot.position, Vector3.down * leftFootoffset, out hit, leftFootoffset))
        {
            FootStepLeft = true;
        }
        else
        {
            FootStepLeft = false;
        }
    }

    private void RightFootStep()
    {
        footStep.transform.position = rightFoot.position;
        footStep.Play();
    }

    private void LeftFootStep()
    {
        footStep.transform.position = leftFoot.position;
        footStep.Play();
    }


    public bool FootStepRight { 
        get => footStepRight;
        set
        {
            if (value && footStepRight == false)
                RightFootStep();
            footStepRight = value;
        }
    }
    public bool FootStepLeft 
    {
        get => footStepLeft;
        set
        {
            if (value && footStepLeft == false)
                LeftFootStep();
            footStepLeft = value;
        }
    }
}
