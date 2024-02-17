using _Root.Scripts.Enums;
using JetBrains.Annotations;
using UnityEngine;

namespace _Root.Scripts.View
{
    public class PointView : MonoBehaviour
    {
        [SerializeField] private Transform _point1;
        [SerializeField] private Transform _point2;
        [SerializeField] [CanBeNull] private Transform _point3;
        [SerializeField] [CanBeNull] private Transform _point4;
        [SerializeField] private FieldEnum _fieldEnum;

        private bool _isWrite = false;

        public Transform Point1 => _point1;
        public Transform Point2 => _point2;
        public Transform Point3 => _point3;
        public Transform Point4 => _point4;
        public FieldEnum FieldEnum => _fieldEnum;
        public bool IsWrite => _isWrite;

        public void SetIsWrite()
        {
            _isWrite = true;
        }
        public void SetFieldEnum(FieldEnum fieldEnum)
        {
            _fieldEnum = fieldEnum;
        }

        public void OnDestroy()
        {
            if (this.gameObject != null)
                Destroy(this.gameObject, 1);
        }
    }
}