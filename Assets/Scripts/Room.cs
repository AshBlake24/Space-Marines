using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<ExitPoint> _exitPoints;
    [SerializeField] private GameObject _entryPoint;

    public void Init(ExitPoint connectingPoint)
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.y + connectingPoint.Rotation, 0);

        transform.position = Vector3.MoveTowards(transform.position, _entryPoint.transform.position, -GetShiftDistance());
    }

    public ExitPoint SelectExitPoint()
    {
        ExitPoint connectingPoint = _exitPoints[Random.Range(0, _exitPoints.Count)];

        while (connectingPoint.IsNextZoneFull(this) == true)
        {
            _exitPoints.Remove(connectingPoint);
            connectingPoint.Hide();

            if (gameObject.name != "Corridor(Clone)")
                Debug.Log("Cast");

            if (_exitPoints.Count != 0)
                connectingPoint = _exitPoints[Random.Range(0, _exitPoints.Count)];
            else
                break;
        }

        if (_exitPoints.Count != 0)
            _exitPoints.Remove(connectingPoint);

        return connectingPoint;
    }

    public void HideExit()
    {
        foreach (var exitPoint in _exitPoints)
        {
            exitPoint.Hide();
        }
    }


    public float GetShiftDistance()
    {
        return Vector3.Distance(transform.position, _entryPoint.transform.position);
    }
}
