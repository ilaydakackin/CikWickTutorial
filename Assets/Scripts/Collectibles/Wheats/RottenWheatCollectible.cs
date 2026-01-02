using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour, ICollectible
{
   
   [SerializeField] private PlayerController _playerController;
   [SerializeField] private float _movementDecreaseSpeed;
   //_resetBoostDuration, ne kadar zaman sonra bitecek?
   [SerializeField] private float _resetBoostDuration;
   public void Collect()
    {
        _playerController.SetMovementSpeed(_movementDecreaseSpeed, _resetBoostDuration);

        //Destroy, karakter coini topladığında coini yok etmek için kullandım.
        Destroy(this.gameObject);
    }
}
