using Enums;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InteractionWindow : Window
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _tip;

        public override WindowType Type => WindowType.Interaction;


        public void Show(IInteractable interactable)
        {
            _image.sprite = interactable.Image;
            _tip.text = interactable.Action + " - E";
            base.Show();
        }
    }
}