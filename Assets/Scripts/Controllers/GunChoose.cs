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

        [Header("Shells Setup")]

        [SerializeField] private List<Interaction.GunAvalibleShell> _shells;
        [SerializeField] private Transform _shellPos;

        [Header("UI Setup")]

        [SerializeField] private GameObject _GunChoosePanel;
        [SerializeField] private GameObject _ShellChoosePanel;
        [SerializeField] private GameObject _ShootPanel;

        [Header("Other")]

        [SerializeField] private GunController _gunController;

        private int _curGunPos;
        private int _curShellPos;
        private GameObject _curObj;

        private void Start()
        {
            _curGunPos = 0;
            _curShellPos = 0;
            CreateGun(_curGunPos);
        }

        public void ChangeGun(int posPlus = 1)
        {
            Destroy(_curObj);
            _curGunPos += posPlus;

            if (_curGunPos < 0)
                _curGunPos = _guns.Count-1;
            else if (_curGunPos > _guns.Count-1)
                _curGunPos = 0;

            CreateGun(_curGunPos);
        }

        public void ChangeShell(int posPlus = 1)
        {
            Destroy(_curObj);
            _curShellPos += posPlus;

            if (_curShellPos < 0)
                _curShellPos = _shells[_curGunPos].sourcePrefabs.Count - 1;
            else if (_curShellPos > _shells[_curGunPos].sourcePrefabs.Count - 1)
                _curShellPos = 0;

            CreateShell(_curShellPos);
        }

        public void ConfirmGun()
        { 
            _GunChoosePanel.SetActive(false);
            _ShellChoosePanel.SetActive(true);
            _gunController.gun = _curObj.GetComponent<Interaction.Shooting>();
            
            CreateShell(_curShellPos);
        }

        public void ConfirmShell()
        {
            _ShellChoosePanel.SetActive(false);
            _ShootPanel.SetActive(true);
            _gunController.gun.ChangeShell(_shells[_curGunPos].sourcePrefabs[_curShellPos]);
            _gunController.barrel = _gunController.gun.barrel;
            Destroy(_curObj);
        }

        private void CreateGun(int index) => _curObj =  Instantiate(_guns[index], _gunPos);
        private void CreateShell(int index)
        {
            _curObj = Instantiate(_shells[_curGunPos].sourcePrefabs[index], _shellPos);
            _curObj.transform.rotation = new Quaternion();
            _curObj.GetComponent<Interaction.Shell>().GetRb().isKinematic = true;
        }
    }
}

