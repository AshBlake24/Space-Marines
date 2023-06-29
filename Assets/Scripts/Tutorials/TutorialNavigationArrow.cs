using UnityEngine;

namespace Roguelike.Tutorials
{
    public class TutorialNavigationArrow : MonoBehaviour
    {
        private TutorialNavigationPoint[] _navigationPoints;
        private Transform _currentNavigationPoint;
        private int _currentNavigationPointIndex;

        public void Construct(TutorialNavigationPoint[] navigationPoints)
        {
            _navigationPoints = navigationPoints;

            foreach (TutorialNavigationPoint navigationPoint in _navigationPoints)
                navigationPoint.Interacted += OnInteracted;
        }

        private void Start()
        {
            if (_navigationPoints == null || _navigationPoints.Length == 0)
                Destroy(gameObject);
            else
                StartRoute();
        }

        private void Update()
        {
            Vector3 direction = _currentNavigationPoint.position - transform.position;
            direction.y = transform.position.y;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }

        private void SetNextDestination(int routeIndex)
        {
            routeIndex++;

            if (routeIndex >= _navigationPoints.Length) 
                Destroy(gameObject);

            _currentNavigationPoint = _navigationPoints[routeIndex].transform;
        }

        private void StartRoute()
        {
            _currentNavigationPointIndex = 0;
            _currentNavigationPoint = _navigationPoints[_currentNavigationPointIndex].transform;
            enabled = true;
        }

        private void OnInteracted(int routeIndex) => SetNextDestination(routeIndex);
    }
}