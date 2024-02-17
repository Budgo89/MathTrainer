using System;
using TMPro;
using UnityEngine;

namespace _Root.Scripts.View
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private float _value;
        public event Action<float> _action;

        private void OnTriggerEnter(Collider other)
        {
            _action?.Invoke(_value);
        }

        public void SetValue(float value)
        {
            _value = value;
        }

        public float GetValue()
        {
            return _value;
        }

        private void Update()
        {
            if (_value == 0)
            {
                _text.text = "";
            }
            else
            {
                
                if (_value % 1 == 0)
                {
                    _text.text = _value.ToString();
                }
                else
                {
                    _text.text = _value.ToString("0.00");
                }
            }
                
        }
    }
}