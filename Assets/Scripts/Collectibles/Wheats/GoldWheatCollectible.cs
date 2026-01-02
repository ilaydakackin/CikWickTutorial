using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour, ICollectible
{
   [SerializeField] private PlayerController _playerController;
   [SerializeField] private float _movementIncreaseSpeed;
   //_resetBoostDuration, ne kadar zaman sonra bitecek?
   [SerializeField] private float _resetBoostDuration;
   public void Collect()
    {
        _playerController.SetMovementSpeed(_movementIncreaseSpeed, _resetBoostDuration);

        //Destroy, karakter coini topladığında coini yok etmek için kullandım.
        Destroy(this.gameObject);
    }
}
