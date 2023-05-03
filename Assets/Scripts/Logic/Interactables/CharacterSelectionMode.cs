using System;
using Cinemachine;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.UI.Windows;
using UnityEngine;

namespace Roguelike.Logic.Interactables
{
    public class CharacterSelectionMode : MonoBehaviour
    {
        private const int RayMaxDistance = 100;

        [SerializeField] private CinemachineVirtualCamera _topDownCamera;
        [SerializeField] private CinemachineVirtualCamera _characterSelectionCamera;
        [SerializeField] private CinemachineVirtualCamera _playerCamera;

        private IStaticDataService _staticData;
        private IWindowService _windowService;
        private IGameFactory _gameFactory;
        private IWeaponFactory _weaponFactory;
        private ISaveLoadService _saveLoadService;
        private RaycastHit _raycastHit;
        private Camera _camera;
        private BaseWindow _selectionWindow;
        private bool _isActive;
        private bool _characterSelected;

        public void Construct(IGameFactory gameFactory, IStaticDataService staticData, IWindowService windowService,
            ISaveLoadService saveLoadService, IWeaponFactory weaponFactory, BaseWindow selectionWindow)
        {
            _staticData = staticData;
            _windowService = windowService;
            _gameFactory = gameFactory;
            _weaponFactory = weaponFactory;
            _saveLoadService = saveLoadService;
            _selectionWindow = selectionWindow;
            _isActive = true;
            _characterSelected = false;
        }

        private void Start() =>
            _camera = Camera.main;

        private void Update()
        {
            if (_isActive == false)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out _raycastHit, RayMaxDistance))
                {
                    if (_raycastHit.collider.TryGetComponent(out SelectableCharacter character))
                    {
                        BaseWindow window = _windowService.Open(WindowId.CharacterStats);

                        if (window is CharacterStats characterStats)
                            characterStats.Construct(
                                character.Id,
                                this,
                                _staticData,
                                _weaponFactory);
                        else
                            throw new ArgumentNullException(nameof(window), "The necessary component is missing");

                        ZoomIn(_raycastHit.collider.transform);
                    }
                }
            }
        }

        public void OnCharacterSelected()
        {
            Transform spawnPoint = _raycastHit.transform;
            Destroy(_raycastHit.collider.gameObject);

            GameObject player = InitPlayer(spawnPoint);
            InitHud(player);
            SetPlayerCamera(player);

            _saveLoadService.InformProgressReaders();
            _characterSelected = true;
            enabled = false;
        }

        public void ZoomOut()
        {
            if (_characterSelected)
                return;

            _isActive = true;
            _selectionWindow.gameObject.SetActive(true);
            _topDownCamera.enabled = true;
        }

        private void ZoomIn(Transform character)
        {
            _isActive = false;
            _selectionWindow.gameObject.SetActive(false);
            _characterSelectionCamera.Follow = character;
            _characterSelectionCamera.LookAt = character;
            _topDownCamera.enabled = false;
        }

        private void SetPlayerCamera(GameObject player)
        {
            _topDownCamera.enabled = false;
            _characterSelectionCamera.enabled = false;
            _playerCamera.enabled = true;
            _playerCamera.Follow = player.transform;
            _playerCamera.LookAt = player.transform;
        }

        private GameObject InitPlayer(Transform spawnPoint) =>
            _gameFactory.CreatePlayer(spawnPoint);

        private void InitHud(GameObject player) =>
            _gameFactory.CreateHud(player);
    }
}