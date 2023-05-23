using Roguelike.Logic.Interactables;
using Roguelike.Player;
using Roguelike.StaticData.Levels;
using UnityEngine;

namespace Roguelike.Logic.Portal
{
    public class PortalConsole : MonoBehaviour, IInteractable
    {
        [SerializeField] private Outline _outline;
        [SerializeField] private Portal _portal;
        [SerializeField] private ParticleSystem _holoMap;

        public Outline Outline => _outline;
        public bool IsActive { get; private set; }

        private void OnEnable()
        {
            Outline.enabled = false;
            IsActive = true;
        }

        public void Interact(GameObject interactor)
        {
            if (interactor.TryGetComponent(out PlayerHealth player))
            {
                _holoMap.Play();
                _portal.Init(LevelId.Dungeon, StageId.Level11);
            }
        }
    }
}