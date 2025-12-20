using NUnit.Framework;
using UnityEngine;

public class CollisionTriggerTrest : MonoBehaviour
{
    //Nesne bu özellikle birlikte temas eder ve içinden geçmez
    //Kurşunun duvara çarpması gibi işlemlerde kullanıulır
    private void OnCollisionEnter(Collision collision)
    {
        
    }

    //Collider'deki is Triger görevi görür. Yani nesneler aslında birbiririne çarpsa da içinden geçip gider
    //Bu özelliği oyunlarda Coin toplama gibi işlemlerde kullanırız
    private void OnTriggerEnter(Collider other)
    {
        
    }

 
}
