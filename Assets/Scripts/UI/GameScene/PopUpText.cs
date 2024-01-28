using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.UI
{
    public class PopUpText : NetworkBehaviour
    {
        [Header("Animation Parameters")]
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _upY;
        [SerializeField] private AnimationCurve _hideAnimation;
        [Space]

        [SerializeField] private TextMeshProUGUI _text;

        private Transform _cameraTransform;
        
        private float _currentTime;
        private float _initY;

        public event Action<PopUpText> Reseted;

        public void Init(Transform camera, string text)
        {
            _cameraTransform = camera;
            _currentTime = 0;
            _initY = transform.position.y;
            _text.SetText(text);
            enabled = true;
        }

        public void Update()
        {
            if (_currentTime < _lifeTime)
            {
                float percent = Mathf.Clamp01(_currentTime / _lifeTime);

                transform.position = new Vector3(transform.position.x, _initY + (_upY * percent), transform.position.z);
                float a = 1f - _hideAnimation.Evaluate(percent);
                _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, a);

                transform.rotation = Quaternion.LookRotation(transform.position - _cameraTransform.position);
                _currentTime += Time.deltaTime;
            }
            else
            {
                Reset();
            }
        }

        private void Reset()
        {
            _currentTime = 0f;
            Reseted?.Invoke(this);
            enabled = false;
        }
    }
}
