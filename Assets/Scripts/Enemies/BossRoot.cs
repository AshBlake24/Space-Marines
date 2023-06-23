using Cinemachine;
using Roguelike.UI.Elements;
using UnityEngine;

namespace Roguelike.Enemies
{
    public class BossRoot : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _bossCamera;
        [SerializeField] private ActorUI _healthBar;
        [SerializeField] private Transform _cameraPoint;

        public CinemachineVirtualCamera Camera => _bossCamera;
        public ActorUI HealthBar => _healthBar;
        public Transform CameraPoint => _cameraPoint;

        public void Init(Enemy boss)
        {
            CreateBossCamera();
            CreateHealthBar(boss);
        }

        private void CreateBossCamera()
        {
            _bossCamera = Instantiate(_bossCamera);
            _bossCamera.gameObject.SetActive(false);
        }

        private void CreateHealthBar(Enemy boss)
        {
            GameObject hud = Object.FindObjectOfType<ActorUI>().gameObject;

            _healthBar = Object.Instantiate(_healthBar, hud.transform);
            _healthBar.Construct(boss.Health);
            _healthBar.gameObject.SetActive(false);
        }
    }
}