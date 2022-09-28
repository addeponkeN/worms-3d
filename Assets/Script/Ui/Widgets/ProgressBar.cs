using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Widgets
{
    public class ProgressBar : MonoBehaviour
    {
        public Image ImgBackground => _imgBackground;
        public Image ImgFill => _imgFill;
        
        public float Value
        {
            get => Slider.value;
            set => Slider.value = value;
        }
        
        [NonSerialized] public Slider Slider;
        
        [NonSerialized] protected RectTransform TfRect;

        [SerializeField] private Image _imgBackground;
        [SerializeField] private Image _imgFill;

        protected virtual void Awake()
        {
            TfRect = GetComponent<RectTransform>();
            Slider = GetComponent<Slider>();
            Slider.value = 1f;
        }

        protected virtual void Update()
        {
        }
        
    }
}