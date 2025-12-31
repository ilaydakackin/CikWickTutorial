using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Fizik Motoru Algıladı: " + other.gameObject.name);
        if(other.CompareTag(Consts.WheatTypes.GOLD_WHEAT))
        {
           other.gameObject?.GetComponent<GoldWheatCollectible>().Collect();
        }

         if(other.CompareTag(Consts.WheatTypes.HOLY_WHEAT))
        {
             other.gameObject?.GetComponent<HolyWheatCollectible>().Collect();
        }

         if(other.CompareTag(Consts.WheatTypes.ROTTEN_WHEAT))
        {
             other.gameObject?.GetComponent<RottenWheatCollectible>().Collect();
        }    
    }
}
