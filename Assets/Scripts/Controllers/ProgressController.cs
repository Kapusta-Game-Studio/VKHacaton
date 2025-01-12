using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Controllers
{
    public class ProgressController : MonoBehaviour
    {
        [Header("Count of troops")]
        [SerializeField] internal int neededCount;

        [Header("UI Setup")]
        [SerializeField] private TextMeshProUGUI _killedText;
        [SerializeField] private string _KilledTextTemplate;
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _shootPanel;

        internal bool gameProcessing { get; private set; }
        internal bool isKillCamShowing;

        private int _curCount;

        private void Awake()
        {
            UpdateText();
            _winPanel.SetActive(false);
            gameProcessing = true;
            isKillCamShowing = false;
            _curCount = 0;
        }

        private void UpdateText() => _killedText.text = _KilledTextTemplate + '\n' + _curCount.ToString() + "/" + neededCount.ToString();
        
        internal void TargetDestroyed()
        {
            _curCount++;
            UpdateText();
            if (_curCount == neededCount)
            {
                gameProcessing = false;
                _shootPanel.SetActive(false);
                _winPanel.SetActive(true);
            }
        }
    }
}
