using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField]
    Transform mInteractPoint;

    [SerializeField]
    float mInteractRadius;

    [SerializeField]
    LayerMask mInteractableLayer;


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire2"))
        {
            Interact();
        }
    }

    private void Interact()
    {
         Collider[] interactables = Physics.OverlapSphere(mInteractPoint.position, mInteractRadius,mInteractableLayer);


        //damage detected enemies
        foreach(Collider interactable in interactables)
        {

            var objectTag = interactable.tag;
            if(objectTag == "essence")
            {
                var essence = interactable.GetComponent<Essence>();
                if(essence != null)
                    essence.Interact();
            }
                   

        //     var interact = interactable.GetComponent<Interact>();
        //     if(interact != null)
        //         interact.interact();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(mInteractPoint.position, mInteractRadius);
    }
    public float getInteractRadius()
    {
        return mInteractRadius;
    }

}
