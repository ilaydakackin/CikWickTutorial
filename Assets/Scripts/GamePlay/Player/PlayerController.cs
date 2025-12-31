using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Event Mantığı nedir? 
    //örneğin zıplandığında ya da bir odaya girildiğinde bu event tetiklenecek sonra animationcontrollera
    //gidip karakter zıpladığında şunu yap ya da karakter odaya girince şunu yap gibi işlemleri gerçekleştireceğiz   
    //Start fonksiyonunda çağrılması ömnerilir 

    //OnPlayerJumped: karakterim zıpladığında                                                                                                                                                                     
    public event Action OnPlayerJumped;
    [Header("Referances")]
    [SerializeField] private Transform _orientationTransform;


    //hareket ayarları      
    [Header("Movement Settings")]
    [SerializeField] private KeyCode _movementKey;
    [SerializeField] private float _movementSpeed;
    private StateController _stateController;
    private Rigidbody _playerRigitbody;
    private float _horizontalInput, _verticalInput;
    private Vector3 _movementDirection;
    private bool _isSliding = false;


    //Zıplama Ayarları
    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCoolDown;
    [SerializeField] private float _airMultipler;
    [SerializeField] private float _airDrag;

    [SerializeField] private bool _canJump;

    
    //Kayma Ayarları
    [Header("Sliding Settings")]
    [SerializeField] private KeyCode _slideKey;
    [SerializeField] private float _slideMultiplier;
    [SerializeField] private float _slideDrag;


    //Yer Ayarları
    [Header("Ground Check Settings")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundDrag;

    private void Awake()
    {
        _stateController = GetComponent<StateController>();
        _playerRigitbody = GetComponent<Rigidbody>();
        _playerRigitbody.freezeRotation = true;
        //Unity'nin fizik motoru, bir nesne bir köşeye çarptığında veya hareket ederken onu doğal olarak döndürmeye çalışır.
        //Eğer bunu yapmazsan, karakterin yürürken bir engele çarptığında top gibi yuvarlanmaya başlar.
        //ama bu işlem sonucunda _playerRigitbody.freezeRotation = true; Karakterin fiziksel olarak etkileşime girer ama dik duruşunu asla bozmaz.
    }

    //Update: Bilgisayarın ekran yenileme hızına (FPS) bağlıdır. Tuş basışlarını kaçırmamak için en ideal yerdir.
    private void Update()
    {
        SetInputs();
        SetStates();
        SetPlayerDrag();
        LimitPlayerSpeed();
    }

    //FixedUpdate: Saniyede sabit 50 kez çalışır. Fizik hesaplamaları (AddForce gibi) burada yapılmalıdır; 
    // aksi takdirde karakter yüksek FPS'li bilgisayarlarda çok hızlı, düşük FPS'li bilgisayarlarda yavaş hareket eder.
    private void FixedUpdate()
    {
        SetPlayerMovement();
    }
    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(_slideKey))
        {
            _isSliding = true;    
        }
        else if(Input.GetKeyDown(_movementKey))
        {
            _isSliding = false;   
        }
        //eğer _jumpKey'e basarsam önce bu 3 koşula bakacak
        //_jumpKey zıplama tuşuna bastım mı?
        //_canJump zıplayabilir miyim?
        //IsGraunded, Şu an yerde mi havada mı?
        else if (Input.GetKey(_jumpKey)&& _canJump && IsGrounded())
        {
            //Zıplama işlemi gerçekleşecek
            _canJump = false;
            SetPlayerJumping();
            Invoke(nameof(ResetJumping), _jumpCoolDown);
        }        
    }


    private void SetStates()
    {
        var movementDirection = GetMovementDirection();
        var isGrounded = IsGrounded();
        var isSliding = IsSliding();
        var currentState = _stateController.GetCurrentState();
       

      var newState = currentState switch
    {
        //Öncelik Sırası Önemli!
        
        // 1. Zıplama (En öncelikli)
        _ when !_canJump && !isGrounded => PlayerState.Jump,

        // 2. Kayma (Hareket ediyor, yerde ve slide tuşuna basılmış)
        // BURADAKİ HATA DÜZELTİLDİ: !isSliding yerine isSliding kullanıldı.
        _ when movementDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,

        // 3. Koşma (Hareket ediyor, yerde ve slide tuşuna BASILMAMIŞ)
        _ when movementDirection != Vector3.zero && isGrounded && !isSliding => PlayerState.Move,

        // 4. Durma (Hareket yok ve yerde)
        _ when movementDirection == Vector3.zero && isGrounded => PlayerState.Idle,

        _ => currentState
    };

        if(newState != currentState)
        {
            _stateController.ChangeState(newState);
        }
        Debug.Log(newState);
    }

    //Hareket Yönleri x,y ve z
    //Orientation (Yönelim): Karakterin baktığı yöne göre "ileri" gitmesini sağlar. Eğer sadece Vector3.forward deseydin,
    //  karakter dünya eksenine göre hep kuzeye giderdi. Bu satır sayesinde, kamerayı çevirdiğinde "ileri" tuşu kameranın baktığı yöne dönüşür.

    //normalized: Eğer aynı anda hem W hem D tuşuna (çapraz) basarsan, vektörün boyu uzar ve karakter daha hızlı gider. 
    //normalized kullanarak her yöne eşit hızla (hızı 1 birime sabitleyerek) gitmesini sağlıyorsun.
    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput
         + _orientationTransform.right * _horizontalInput;
        
        //zıplarken nasıl bir hızda olşduğunu belirlemek için
        float forceMultiplier = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Slide => _slideMultiplier,
            PlayerState.Jump => _airMultipler,
            _ => 1f
        };
         _playerRigitbody.AddForce(_movementDirection.normalized * _movementSpeed * forceMultiplier, ForceMode.Force);
        //eğer kayıyorsam _sildeMultiplieri de çarçarak kayma işlemi yap
       
       
    }
    private void SetPlayerDrag()
    {
        _playerRigitbody.linearDamping = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => _groundDrag,
            PlayerState.Slide => _slideDrag,
            PlayerState.Jump => _airDrag,
            _ => _playerRigitbody.linearDamping

        };
       
    }

    //karakterin hızının sınırının ayarlandığı method
    //oyunda hile yapılmaması gibi durumlar için yapıyoruz
      private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(_playerRigitbody.linearVelocity.x, 0f , _playerRigitbody.linearVelocity.z);
        //hareketin büyüklüğü bizim belirlediğimiz _movementSpeed den fazla ise
        if(flatVelocity.magnitude > _movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
            _playerRigitbody.linearVelocity = new Vector3 (limitedVelocity.x, _playerRigitbody.linearVelocity.y, _playerRigitbody.linearVelocity.z);
        }
    } 
    
    //Zıplama Mantığı: Neden Velocity'yi Sıfırlıyoruz? şu satıra dikkat: _playerRigitbody.linearVelocity = new Vector3(..., 0f, ...);
    //Karakterin zaten aşağı doğru düşerken (yerçekimiyle) zıplarsan, yukarı yönlü kuvvet önce o düşme hızını durdurmaya çalışır ve karakter az zıplar. 
    //Çözüm: Zıplamadan hemen önce dikey (Y) hızını sıfırlayarak, her zıplamanın aynı yükseklikte ve tutarlı olmasını sağlıyorsun.
    private void SetPlayerJumping()
    {
        //OnPlayerJuumped null değil ise Event'i tetikliyorum
        OnPlayerJumped?.Invoke();

        _playerRigitbody.linearVelocity = new Vector3(_playerRigitbody.linearVelocity.x, 0f, _playerRigitbody.linearVelocity.z);
        _playerRigitbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }
    private void ResetJumping()
    {
        _canJump = true;
    }

    //RayCast, ışın atar
    //karakterin yerde olup olmadığını bu metodla _playerHeight büyüklüğünde ışın atarak kontrol ediyoruz.Eğer ışın yere değiyorsa True döndürerek Evet yerdesin zıplayabilirsin diyeceğiz
    //eğer karakter yerde değilse zaten havadadır ve zıplayamamasını sağlamamız gerekir
    //Mantık: transform.position karakterin tam merkezidir (göbeği gibi düşün). Boyunun yarısı (0.5f) ayaklarına ulaşır.
    //  Eklediğin 0.2f ise küçük bir tolerans payıdır. Böylece karakter hafif engebeli yerlerde veya merdivenlerdeyken bile "yerdeyim" diyebilir.
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }

    private Vector3 GetMovementDirection()
    {
        return _movementDirection.normalized;
    }
    private bool IsSliding()
    {
        return _isSliding;
    }
}
