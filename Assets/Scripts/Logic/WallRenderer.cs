using UnityEngine;

namespace Roguelike.Logic
{
    public class WallRenderer : MonoBehaviour
    {
        private static readonly int s_color = Shader.PropertyToID("_Color");
        private readonly Color _defaultColor = new(1f, 1f, 1f, 1f);
        private readonly Color _transparentColor = new(1f, 1f, 1f, 0.5f);
        
        [SerializeField] private LayerMask _hideable;
        [SerializeField] private float _rayDistance;
        [SerializeField] private float _timeBetweenRender;

        private Camera _camera;
        private Vector3 _rayOrigin;
        private GameObject _currentObject;
        private MeshConcealer _currentMeshConcealer;
        private MeshConcealer _previousMeshConcealer;

        private void Start()
        {
            _camera = Camera.main;
            _rayOrigin = new Vector3(_camera.pixelWidth / 2f, _camera.pixelHeight / 3f, 0f);
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
            _previousMeshConcealer = _currentMeshConcealer;
            _currentObject = hit.collider.gameObject;
            _currentMeshConcealer = hit.collider.GetComponent<MeshConcealer>();

            for (int i = 0; i < _currentMeshConcealer.Meshes.Length; i++)
            {
                foreach (Material material in _currentMeshConcealer.Meshes[i].materials) 
                    material.SetColor(s_color, _transparentColor);
            }
        }

        private void ClearCurrentRenderer()
        {
            _previousMeshConcealer = _currentMeshConcealer;
            _currentMeshConcealer = null;
            _currentObject = null;
        }

        private void ReturnDefaultColor()
        {
            if (_previousMeshConcealer != null && _previousMeshConcealer != _currentMeshConcealer)
            {
                for (int i = 0; i < _previousMeshConcealer.Meshes.Length; i++)
                {
                    foreach (Material material in _previousMeshConcealer.Meshes[i].materials) 
                        material.SetColor(s_color, _defaultColor);
                }
                
                _previousMeshConcealer = null;
            }
        }
    }
}