using UnityEngine;

public class PlayerClue : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Clue")
        {
            InteractionTrigger trigger = other.GetComponent<InteractionTrigger>();
            if (trigger)
            {
                if (Input.GetKeyDown(trigger.interactionKey))
                {
                    // Todo Destroy 말고 단서보관함으로
                    Destroy(other.gameObject);
                }
            }

        }
    }
    
    
}
