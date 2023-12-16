using System;
using TMPro;
using UnityEngine;

namespace _Root.Scripts.View
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private int _value;
        public event Action<int> _action;

        private void OnTriggerEnter(Collider other)
        {
            _action?.Invoke(_value);
        }

        public void SetValue(int value)
        {
            _value = value;
        }

        public int GetValue()
        {
            return _value;
        }

        private void Update()
        {
            _text.text = _value.ToString();
        }
    }
}