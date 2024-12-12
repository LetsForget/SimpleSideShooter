using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZombieShooter.UI
{
    public class UIPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text bulletsCountLabel;
        
        [SerializeField] private RectTransform losePanel;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button closeButton;

        private void Start()
        {
            losePanel.gameObject.SetActive(false);
        }

        public void SetBulletsCount(int bulletsCount)
        {
            bulletsCountLabel.text = bulletsCount.ToString();
        }

        public void OpenLosePanel(Action restartCallback, Action closeCallback)
        {
            losePanel.gameObject.SetActive(true);
            
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(() => restartCallback?.Invoke());
            
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(() => closeCallback?.Invoke());
        }

        public void ClosePanel()
        {
            losePanel.gameObject.SetActive(false);
        }
    }
}