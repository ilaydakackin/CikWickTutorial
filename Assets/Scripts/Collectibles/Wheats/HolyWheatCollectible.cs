using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesignSO _wheatDesignSO;
   [SerializeField] private PlayerController _playerController;
  
   public void Collect()
    {
        _playerController.SetJumpForce(_wheatDesignSO.IncreaseDecreaseMultiplier, _wheatDesignSO.ReserBoostDuration);

        //Destroy, karakter coini topladığında coini yok etmek için kullandım.
        Destroy(this.gameObject);
    }
}
