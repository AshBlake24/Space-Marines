using Cinemachine;
using UnityEngine;

namespace Roguelike.Logic.Cameras
{
    public class CharacterSelectionMode : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _topDownCamera;
        [SerializeField] private CinemachineVirtualCamera _characterSelectionCamera;
        [SerializeField] private LayerMask _selectablesLayerMask;

        private RaycastHit _raycastHit;
        private Camera _camera;

        private void Start() => 
            _camera = Camera.main;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out _raycastHit, 100, _selectablesLayerMask))
                    ZoomIn(_raycastHit.collider.transform);
                else
                    ZoomOut();

            }
        }

        private void ZoomIn(Transform character)
        {
            _characterSelectionCamera.Follow = character;
            _characterSelectionCamera.LookAt = character;
            _topDownCamera.enabled = false;
        }

        private void ZoomOut() =>
            _topDownCamera.enabled = true;
    }
}