using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform _cameraTarget = null;
    [SerializeField] private Player _player = null;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera = null;
    [SerializeField] private Transform _aim = null;
    [SerializeField] private Camera _mainCamera = null;

    [Tooltip("In viewport distance")][Range(0, 1)]
    [SerializeField] private float _radiusBeforeMoving = 0.0f;
    [Tooltip("In viewport distance")][Range(0, 1)]
    [SerializeField] private float _maxViewDistance = 0.0f;

    private Plane _plane = new Plane(Vector3.up, -1);
    private bool _focus = false;


    private void Awake()
    {
        StartCoroutine(SetCameraPos());  
    }

    public IEnumerator SetCameraPos()
    {
        yield return new WaitForEndOfFrame();
        _virtualCamera.enabled = true;
        _mainCamera.GetComponent<CinemachineBrain>().enabled = true;
    }

    public void Focus(Transform transform)
    {
        _virtualCamera.Follow = transform;
        _virtualCamera.LookAt = transform;

        _focus = true;
    }

    public void ResetToOrigin()
    {
        _virtualCamera.Follow = _cameraTarget;
        _virtualCamera.LookAt = _cameraTarget;

        _focus = false;
    }

    public void WrapCamera()
    {

    }

    private void Update()
    {
        if(!_focus && !_player.Lock)
        {
            Vector3 viewportPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            _aim.position = GetWorldPosition(ray);

            float distanceMousePlayer = Vector2.Distance(new Vector2(viewportPosition.x, viewportPosition.y), new Vector2(0.5f, 0.5f));
            if (distanceMousePlayer > _radiusBeforeMoving)
            {
                Vector3 playerPositionViewport = Vector3.Scale(_mainCamera.WorldToViewportPoint(_player.transform.position), new Vector3(1, 1, 0));
                Vector3 mouseviewportPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);

                Vector3 direction = mouseviewportPosition - new Vector3(0.5f, 0.5f);
                Vector3 cameraTargetViewportPosition = playerPositionViewport + direction.normalized * Mathf.Clamp(direction.magnitude - _radiusBeforeMoving, 0, _maxViewDistance);

                _cameraTarget.position = GetWorldPosition(_mainCamera.ViewportPointToRay(cameraTargetViewportPosition));
            }
            else
            {
                _cameraTarget.position = _player.transform.position;
            }
        }
    }

    private Vector3 GetWorldPosition(Ray ray)
    {
        float enter = 0.0f;
        if (_plane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);

            return hitPoint;
        }

        return Vector3.zero;
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_aim.position, 0.3f);
    }
}
