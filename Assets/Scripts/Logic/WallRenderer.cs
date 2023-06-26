        using UnityEngine;

namespace Roguelike.Logic
{
    public class WallRenderer : MonoBehaviour
    {
        private static readonly int s_color = Shader.PropertyToID("_Color");
        private readonly Color _defaultColor = new(1f, 1f, 1f, 1f);
        private readonly Color _transparentColor = new(1f, 1f, 1f, 0.5f);

        [SerializeField] private LayerMask _hideable;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _rayDistance;
        [SerializeField] private float _timeBetweenRender;

        private GameObject _currentObject;
        private MeshRenderer _currentRenderer;
        private MeshRenderer _previousRenderer;

        private void Start() => 
            InvokeRepeating(nameof(Render), 1f, _timeBetweenRender);

        private void Render()
        {
            Ray ray = new(_cameraTransform.position, _cameraTransform.forward);
            
            if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _hideable))
            {
                if (_currentObject != null && _currentObject == hit.collider.gameObject)
                    return;

                SetObjectTransparent(hit);
            }
            else
            {
                ClearCurrentRenderer();
            }

            ReturnDefaultColor();
        }

        private void SetObjectTransparent(RaycastHit hit)
        {
            _previousRenderer = _currentRenderer;
            _currentObject = hit.collider.gameObject;
            _currentRenderer = hit.collider.GetComponentInParent<MeshRenderer>();
            _currentRenderer.material.SetColor(s_color, _transparentColor);
        }

        private void ClearCurrentRenderer()
        {
            _previousRenderer = _currentRenderer;
            _currentRenderer = null;
            _currentObject = null;
        }

        private void ReturnDefaultColor()
        {
            if (_previousRenderer != null && _previousRenderer != _currentRenderer)
            {
                _previousRenderer.material.SetColor(s_color, _defaultColor);
                _previousRenderer = null;
            }
        }
    }
}