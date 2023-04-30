using Cinemachine;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.Infrastructure.States;
using Roguelike.UI.Windows;
using UnityEngine;

namespace Roguelike.Logic.Cameras
{
    public class CharacterSelectionMode : MonoBehaviour
    {
        private const int RayMaxDistance = 100;
        
        [SerializeField] private CinemachineVirtualCamera _topDownCamera;
        [SerializeField] private CinemachineVirtualCamera _characterSelectionCamera;

        private IStaticDataService _staticData;
        private IWindowService _windowService;
        private RaycastHit _raycastHit;
        private Camera _camera;
        private BaseWindow _selectionWindow;
        private GameStateMachine _gameStateMachine;

        public void Construct(IStaticDataService staticData, IWindowService windowService,
            GameStateMachine gameStateMachine, BaseWindow selectionWindow)
        {
            _staticData = staticData;
            _windowService = windowService;
            _gameStateMachine = gameStateMachine;
            _selectionWindow = selectionWindow;
        }

        private void Start() => 
            _camera = Camera.main;

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
                            characterStats.Construct(_staticData.GetCharacterData(character.Id), this);
                        
                        ZoomIn(_raycastHit.collider.transform);
                    } 
                }
                else
                {
                    ZoomOut();
                } 
            }
        }

        public void OnCharacterSelected()
        {
            
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