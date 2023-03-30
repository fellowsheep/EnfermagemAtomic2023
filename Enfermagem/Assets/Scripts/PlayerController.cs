using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private Transform _startPosition = default;
    [SerializeField] 
    private Transform _endPosition = default;
    [SerializeField] 
    private Transform _tutorialPosition = default;
    [SerializeField] 
    private Transform _playerTransform = default;
    [SerializeField] 
    private float _amountToFill = 0.33f;
    [SerializeField] 
    private bool _isUsingGyroscope = default;
    [SerializeField]
    private float _sensitivity = 2.0f;
    [SerializeField] 
    private Image _interactionImage = default;

    private static Quaternion _cameraOffset;
    private static Quaternion _velocity;
    private static bool _hasReset;
    
    private Vector3 _moveDirection;
    private Quaternion _correctionQuaternion;
    private RaycastHit _raycastHitInfo;
    private GameObject _lastChoice = null;
    private bool _isInterectionComplete = false;
    private float mouseX = 0.0f, mouseY = 0.0f;
    private float minY = -45f;
    private float maxY = 35f;
    
    /*public void ChangeDevice()
    {
        StartCoroutine(LoadDeviceCoroutine("", false));
    }*/

    public void GoToStartLocation()
    {   
        _playerTransform.transform.position = _startPosition.position;
        //_playerTransform.transform.rotation = _startPosition.rotation;
        StartCoroutine(CalibrateYAngleCoroutine());
    }

    public void GoToEndLocation()
    {   
        _playerTransform.transform.position = _endPosition.position;
        //_playerTransform.transform.rotation = _endPosition.rotation;
        _hasReset = true;
        StartCoroutine(CalibrateYAngleCoroutine());
    }

    public void GoToTutorialLocation()
    {
        _playerTransform.transform.position = _tutorialPosition.position;
        //_playerTransform.transform.rotation = _tutorialPosition.rotation;
        StartCoroutine(CalibrateYAngleCoroutine());
        //_playerTransform.GetComponent<Animator>().enabled = true;
    }

    public Transform EndPosition { get => _endPosition; }

    private static Quaternion GyroscopeToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    private IEnumerator Start()
    {
        Input.gyro.enabled = true;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        /*if (!travarMouse)
        {
            return;
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;*/

        // Wait until gyro is active, then calibrate to reset starting rotation.
        if(_hasReset)
        {
            Quaternion gyroQuaternion = GyroscopeToUnity(Input.gyro.attitude);
            Quaternion calculatedRotation = _cameraOffset * gyroQuaternion;
            _playerTransform.rotation = calculatedRotation;
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(.1f);
            StartCoroutine(CalibrateYAngleCoroutine());
        }
    }

    private void Update()
    {
        if(XRSettings.loadedDeviceName != "cardboard")
        {
            if(_isUsingGyroscope)
            {
                GyroscopeModifyCamera();
            }
        }

        //Move();
        CheckInteraction();
        //MouseModifyCam();
    }

    private IEnumerator CalibrateYAngleCoroutine()
    {
        _cameraOffset = _playerTransform.rotation * Quaternion.Inverse(GyroscopeToUnity(Input.gyro.attitude));
        Quaternion gyroQuaternion = GyroscopeToUnity(Input.gyro.attitude);
        Quaternion calculatedRotation = _cameraOffset * gyroQuaternion;
        _playerTransform.rotation = calculatedRotation;
        yield return null;
    }

    private void CalibrateYAngle()
    {
        _cameraOffset = _playerTransform.rotation * Quaternion.Inverse(GyroscopeToUnity(Input.gyro.attitude));
    }

    private void MouseModifyCam()
    {
        mouseX += Input.GetAxis("Mouse X") * _sensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * _sensitivity;
        mouseY = Mathf.Clamp(mouseY, minY, maxY);

        transform.eulerAngles = new Vector3(mouseY, mouseX, 0);
    }

    private IEnumerator LoadDeviceCoroutine(string newDevice, bool enable)
    {
        XRSettings.LoadDeviceByName(newDevice);

        yield return null;

        XRSettings.enabled = enable;
    }

    //private void Move()
    //{
    //    _moveDirection = Input.GetAxisRaw("Horizontal") * transform.right;
    //    _moveDirection += Input.GetAxisRaw("Vertical") * transform.forward;
    //    _moveDirection.Normalize();

    //    transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
    //}

    private void GyroscopeModifyCamera()
    {
        Quaternion gyroQuaternion = GyroscopeToUnity(Input.gyro.attitude);
        
        //Rotate coordinate system 90 degrees. Correction Quaternion has to come first
        Quaternion calculatedRotation = _cameraOffset * gyroQuaternion;  
        _playerTransform.rotation = Quaternion.Slerp(_playerTransform.rotation, calculatedRotation, Input.gyro.rotationRateUnbiased.magnitude);
    }

    private void CheckInteraction()
    {
        if (Physics.Raycast(transform.position, transform.forward, out _raycastHitInfo))
        {
            if(_raycastHitInfo.collider.gameObject.tag == "Interactable")
            {
                if (_raycastHitInfo.collider.gameObject.GetComponent<InteractableChoice>() != null)
                {
                    _raycastHitInfo.collider.gameObject.GetComponent<InteractableChoice>().ChosenEffect();
                    _lastChoice = _raycastHitInfo.collider.gameObject;
                }

                if (!_isInterectionComplete)
                {
                    _interactionImage.fillAmount += _amountToFill * Time.deltaTime;

                    if (_interactionImage.fillAmount >= 1)//Input.GetMouseButtonDown(0))
                    {
                        _isInterectionComplete = true;
                        _raycastHitInfo.collider.gameObject.GetComponent<Interactable>().Interact();
                        _interactionImage.fillAmount = 0.0f;
                    }
                }
            }
            else
            {
                if (_lastChoice != null)
                {
                    _lastChoice.gameObject.GetComponent<InteractableChoice>().RestartScale();
                }
                
                _interactionImage.fillAmount = 0.0f;
                _isInterectionComplete = false;
            }

            if(_raycastHitInfo.collider.gameObject.TryGetComponent(out InteractableScroll scroll))
            {
                scroll.Interact();
            }
        }
        else
        {
            _interactionImage.fillAmount = 0.0f;
            _isInterectionComplete = false;
        }

        //Debug.DrawLine(transform.position, _raycastHitInfo.point, Color.green);
        //Debug.DrawLine(transform.root.position, _forward, Color.green);
    }
}
