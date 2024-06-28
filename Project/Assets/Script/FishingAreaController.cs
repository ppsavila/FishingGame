using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishingAreaController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            other.GetComponent<FishingController>().SetFishingPossible(true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            other.GetComponent<FishingController>().SetFishingPossible(false);
    }

}
