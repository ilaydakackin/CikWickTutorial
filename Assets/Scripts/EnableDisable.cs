using UnityEngine;

public class EnableDisable : MonoBehaviour
{
 //Script aktif olduğunda çalışır
    private void OnEnable()
    {
        Debug.Log("ENABLED");
    }
    private void OnDisable()
    {
    
        Debug.Log("DISABLED");
    }
}
