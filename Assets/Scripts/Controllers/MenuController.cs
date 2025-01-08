using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Controllers
{
    public class MenuController : MonoBehaviour
    {
        [Header ("Text Setup")]
        [SerializeField] private List<string> _eraNames;

        [Header("Objects Setup")]
        [SerializeField] private TextMeshProUGUI _eraText;
        [SerializeField] private List<Button> _btnsStartLevel;

        private int curEraInd;

        private void Start()
        {
            curEraInd = 0;
            ChangeEra(0);
        }

        public void ChangeEra(int changeIndex = 1)
        {
            curEraInd += changeIndex;
            if (curEraInd > _eraNames.Count - 1)
                curEraInd = 0;
            else if (curEraInd < 0)
                curEraInd = _eraNames.Count - 1;

            _eraText.text = _eraNames[curEraInd];

            for (int i = 0; i < _btnsStartLevel.Count; i++)
            {
                Button btn = _btnsStartLevel[i];
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
            }
        }
    }

}

