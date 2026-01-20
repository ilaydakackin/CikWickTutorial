using UnityEngine;

[CreateAssetMenu(fileName ="WheatDesignSO", menuName = "ScriptableObjects/WheatDesignSO")]
public class WheatDesignSO : ScriptableObject
{
   [SerializeField] private float _increaseDecreaseMultiplier;
   [SerializeField] private float _reserBoostDuration;

   //Verileri Public yapmak için alttaki yapıyı kullanıyorunm.
   public float IncreaseDecreaseMultiplier => _increaseDecreaseMultiplier;
   public float ReserBoostDuration => _reserBoostDuration;
}
