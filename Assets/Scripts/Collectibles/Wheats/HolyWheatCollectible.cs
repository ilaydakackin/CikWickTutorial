using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
   
   [SerializeField] private PlayerController _playerController;
   [SerializeField] private float _forceIncrease;
   //_resetBoostDuration, ne kadar zaman sonra bitecek?
   [SerializeField] private float _resetBoostDuration;
   public void Collect()
    {
        _playerController.SetJumpForce(_forceIncrease, _resetBoostDuration);

        //Destroy, karakter coini topladığında coini yok etmek için kullandım.
        Destroy(this.gameObject);
    }
}
