using Cinemachine;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.UI.Windows;
using UnityEngine;

namespace Roguelike.Logic.Cameras
{
    public class CharacterSelectionMode : MonoBehaviour
    {
        private const int RayMaxDistance = 100;
        
        [SerializeField] private CinemachineVirtualCamera _topDownCamera;
        [SerializeField] private CinemachineVirtualCamera _characterSelectionCamera;
        [SerializeField] private GameObject _selectionWindow;

        private IStaticDataService _staticData;
        private IWindowService _windowService;
        private RaycastHit _raycastHit;
        private Camera _camera;

        private void Start()
        {
            _staticData = AllServices.Container.Single<IStaticDataService>();
            _windowService = AllServices.Container.Single<IWindowService>();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out _raycastHit, RayMaxDistance))
                {
                    if (_raycastHit.collider.TryGetComponent(out SelectableCharacter character))
                    {
                        CharacterStats characterStats = _windowService.Open(WindowId.CharacterStats) as CharacterStats;
                        
                        if (characterStats != null)
                            characterStats.Construct( _staticData.GetCharacterData(character.Id), this);
                        
                        ZoomIn(_raycastHit.collider.transform);
                    } 
                }
                else
                {
                    ZoomOut();
                } 
            }
        }

        public void ZoomOut()
        {
            _selectionWindow.gameObject.SetActive(true);
            _topDownCamera.enabled = true;
        }

        private void ZoomIn(Transform character)
        {
            _selectionWindow.gameObject.SetActive(false);
            _characterSelectionCamera.Follow = character;
            _characterSelectionCamera.LookAt = character;
            _topDownCamera.enabled = false;
        }
    }
}