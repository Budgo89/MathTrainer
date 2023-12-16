using _Root.Scripts.Enums;
using UnityEngine;

namespace _Root.Scripts.View
{
    public class PointView : MonoBehaviour
    {
        [SerializeField] private Transform _point1;
        [SerializeField] private Transform _point2;
        [SerializeField] private FieldEnum _fieldEnum;

        public Transform Point1 => _point1;
        public Transform Point2 => _point2;
        public FieldEnum FieldEnum => _fieldEnum;

        public void OnDestroy()
        {
            Destroy(this.gameObject, 1);
        }
    }
}