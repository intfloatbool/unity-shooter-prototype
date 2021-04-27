﻿using UnityEngine;

namespace _Scripts.Common
{
    public class TransformRotationCopier : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        public Transform Target
        {
            get => _target;
            set => _target = value;
        }
        
        private void Update()
        {
            if (_target.Equals(null))
                return;

            transform.rotation = _target.rotation;
        }
    }
}
