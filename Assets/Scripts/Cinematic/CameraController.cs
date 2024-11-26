using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cinematic
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private List<Transform> _camPositions;
        [SerializeField] private Transform _cam;

        private int _curCam;

        private void Start() => _curCam = 0;

        public void ChangeView()
        {
            _curCam = _curCam + 1 < _camPositions.Count ? _curCam+1 : 0;
            _cam.transform.position = _camPositions[_curCam].position;
            _cam.transform.rotation = _camPositions[_curCam].rotation;
        }
    }
}

