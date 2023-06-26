using UnityEngine;

namespace Roguelike.Logic
{
    public class WallRenderer : MonoBehaviour
    {
        private static readonly int s_color = Shader.PropertyToID("_Color");
        private readonly Color _defaultColor = new(1f, 1f, 1f, 1f);
        private readonly Color _transparentColor = new(1f, 1f, 1f, 0.5f);
        private readonly Vector3 _rayOrigin = new(Screen.width / 2f, Screen.height / 2.5f, 0);

        [SerializeField] private LayerMask _hideable;
        [SerializeField] private float _rayDistance;
        [SerializeField] private float _timeBetweenRender;

        private Camera _camera;
        private GameObject _currentObject;
        private MeshRenderer _currentRenderer;
        private MeshRenderer _previousRenderer;

        private void Start()
        {
            _camera = Camera.main;
            InvokeRepeating(nameof(Render), 1f, _timeBetweenRender);
        }

        private void Render()
        {
            Ray ray = _camera.ScreenPointToRay(_rayOrigin);

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

            foreach (Material material in _currentRenderer.materials) 
                material.SetColor(s_color, _transparentColor);
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
                foreach (Material material in _previousRenderer.materials) 
                    material.SetColor(s_color, _defaultColor);
                
                _previousRenderer = null;
            }
        }
    }
}