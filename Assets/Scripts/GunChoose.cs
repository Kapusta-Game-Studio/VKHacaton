using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class GunChoose : MonoBehaviour
    {
        [Header("Guns Setup")]

        [SerializeField] private List<GameObject> _guns;
        [SerializeField] private Transform _gunPos;

        [Header("UI Setup")]

        [SerializeField] private GameObject _GunChoosePanel;
        [SerializeField] private GameObject _ShootPanel;

        [Header("Other")]

        [SerializeField] private GunController _gunController;

        private int _curPos;
        private GameObject _curGun;

        private void Start()
        {
            _curPos = 0;
            CreateGun(_curPos);
        }

        public void ChangeGun(int posPlus = 1)
        {
            Destroy(_curGun);
            _curPos += posPlus;

            if (_curPos < 0)
                _curPos = _guns.Count-1;
            else if (_curPos > _guns.Count-1)
                _curPos = 0;

            CreateGun(_curPos);
        }

        public void ConfirmGun()
        {
            _GunChoosePanel.SetActive(false);
            _ShootPanel.SetActive(true);
            _gunController.gun = _curGun.GetComponent<Interaction.Shooting>();
        }

        private void CreateGun(int index) => _curGun =  Instantiate(_guns[index], _gunPos);
    }
}

