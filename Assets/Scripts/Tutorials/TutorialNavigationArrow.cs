using System;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Tutorials
{
    public class TutorialNavigationArrow : MonoBehaviour
    {
        private List<TutorialNavigationPoint> _navigationPoints;
        private Transform _currentNavigationPoint;
        private int _currentNavigationPointIndex;

        public void Construct(List<TutorialNavigationPoint> navigationPoints)
        {
            _navigationPoints = navigationPoints;
            StartRoute();
        }

        private void Update()
        {
            Vector3 direction = _currentNavigationPoint.position - transform.position;
            direction.y = transform.position.y;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }

        public void SetNextDestination()
        {
            _currentNavigationPointIndex++;

            if (_currentNavigationPointIndex >= _navigationPoints.Count) 
                Destroy(gameObject);

            _currentNavigationPoint = _navigationPoints[_currentNavigationPointIndex].transform;
        }

        private void StartRoute()
        {
            if (_navigationPoints.Count <= 0)
                throw new ArgumentOutOfRangeException(nameof(_navigationPoints));

            _currentNavigationPointIndex = 0;
            _currentNavigationPoint = _navigationPoints[_currentNavigationPointIndex].transform;
            enabled = true;
        }
    }
}