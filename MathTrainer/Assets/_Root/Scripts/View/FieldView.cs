using System.Collections.Generic;
using UnityEngine;

namespace _Root.Scripts.View
{
    public class FieldView : MonoBehaviour
    {
        [SerializeField] private List<Transform> _points;
        [SerializeField] private Transform _point1;
        [SerializeField] private Transform _point2;
        [SerializeField] private Transform _point3;
        [SerializeField] private Transform _point4;

        public Transform Point1 => _point1;
        public Transform Point2 => _point2;
        public Transform Point3 => _point3;
        public Transform Point4 => _point4;
        public List<Transform> Points => _points;

    }
}