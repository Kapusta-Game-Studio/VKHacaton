using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cinematic
{
    public class CameraController : MonoBehaviour
    {
        private const float KILL_CAM_SECONDS_AWAIT = 2f;

        [SerializeField] private List<Transform> _camPositions;
        [SerializeField] private Transform _cam;
        [SerializeField] private Transform _cinematicObj;
        [SerializeField] private GameObject _shootPanel;
        [SerializeField] private Controllers.ProgressController _processController;

        private int _curCam;

        private void Start() => _curCam = 0;

        public void ChangeView()
        {
            _curCam = _curCam + 1 < _camPositions.Count ? _curCam+1 : 0;
            MoveCamera();
        }

        public void KillCamShow(Transform pos, Transform shell)
        {
            _processController.isKillCamShowing = true;
            _shootPanel.SetActive(false);
            _cam.transform.position = pos.position;
            _cam.transform.parent = pos;
            _cam.transform.LookAt(shell);

        }
        public void KillCamRemoval()
        {
            _cam.transform.parent = null;
            StartCoroutine(WaitAndReturnCam());
        }
        private void MoveCamera()
        {
            _cam.transform.position = _camPositions[_curCam].position;
            _cam.transform.rotation = _camPositions[_curCam].rotation;
        }
        private IEnumerator WaitAndReturnCam()
        {
            yield return new WaitForSeconds(KILL_CAM_SECONDS_AWAIT);
            _processController.isKillCamShowing = false;
            _shootPanel.SetActive(true);
            MoveCamera();
        }
    }
}

