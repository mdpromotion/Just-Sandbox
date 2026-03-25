using System.Collections;
using UnityEngine;

namespace Feature.Agent.Infrastructure
{
    [RequireComponent(typeof(Renderer))]
    public class MaterialController : MonoBehaviour
    {
        private Renderer _renderer;

        private Color _defaultColor;
        private Color _damageColor = Color.red;

        private bool _isHurt;
        private float _hurtTimer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _defaultColor = _renderer.material.color;
        }

        public void Hurt()
        {
            _isHurt = true;
            _hurtTimer = 0.3f;
            _renderer.material.color = _damageColor;
        }

        private void Update()
        {
            if (_isHurt)
            {
                _hurtTimer -= Time.deltaTime;
                if (_hurtTimer <= 0f)
                {
                    _renderer.material.color = _defaultColor;
                    _isHurt = false;
                }
            }
        }
    }
}