using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
[Header("Referances")]
[SerializeField] private Transform _playerTransform;
[SerializeField] private Transform _orientationTransform;
[SerializeField] private Transform _playerVisualTransform;

[Header("Settings")]
[SerializeField] private float _rotationSpeed;
    private void Update()
    {
        //viewDirection = baktığımız pozisyon
        Vector3 viewDirection =
            _playerTransform.position - new Vector3(transform.position.x, _playerTransform.position.y, transform.position.z);

        //oryantasyonum baktığım yere baksın
        _orientationTransform.forward = viewDirection.normalized;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        //Yeni bir input direction oluşturduk ve bunu yine klatrakter kontrolümüzdeki gibi yaptık
        Vector3 inputDirection = 
            _orientationTransform.forward * verticalInput + _orientationTransform.right* horizontalInput;

    //inputdirection = dan farkli ise Slerp ile karakterin rotasyon işlemlerini yumuşattık
    if(inputDirection != Vector3.zero)
        {
             //Karakterin rotasyon işlemi
             _playerVisualTransform.forward
            = Vector3.Slerp(_playerVisualTransform.forward, inputDirection.normalized, Time.deltaTime * _rotationSpeed); 
        }
        
    }
}
