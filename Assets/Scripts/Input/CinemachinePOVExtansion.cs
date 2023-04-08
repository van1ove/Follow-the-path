using UnityEngine;
using Cinemachine;
public class CinemachinePOVExtansion : CinemachineExtension
{
    [SerializeField] private float _speed;
    [SerializeField] private float _clampAngle;
    private InputManager _inpManager;
    private Vector3 _startingRotation; 
    protected override void Awake() 
    {
        _inpManager = InputManager.Instance;
        if(_startingRotation == null)
        {
            _startingRotation = transform.localRotation.eulerAngles;
        }

        base.Awake();    
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if(vcam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                Vector2 mouseImput = _inpManager.GetMouseDelta();
                _startingRotation.x += mouseImput.x * Time.deltaTime * _speed;
                _startingRotation.y -= mouseImput.y * Time.deltaTime * _speed;
                _startingRotation.y = Mathf.Clamp(_startingRotation.y, -_clampAngle, _clampAngle);

                state.RawOrientation = Quaternion.Euler(_startingRotation.y, _startingRotation.x, 0f);
            }
        }
    }
}
