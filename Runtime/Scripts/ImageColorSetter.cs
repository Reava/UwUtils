using UdonSharp;
using UnityEngine;
using UnityEngine.UI;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/UI Color Setter")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ImageColorSetter : UdonSharpBehaviour
    {
        public Image[] imgSources;
        public Color colorDefault;
        public Color colorSet;

        public int selectionId = 0;

        public void _setImgColor()
        {
            foreach (var img in imgSources)
            {
                img.color = colorSet;
            }
        }
        public void _resetImgColor()
        {
            foreach (var img in imgSources)
            {
                img.color = colorDefault;
            }
        }

        public override void Interact()
        {
            _toggleColor();
        }

        public void _toggleColor()
        {
            selectionId = selectionId == 1 ? 0 : 1;

            _SelectionChanged();
        }

        public void _SelectionChanged()
        {
            if (selectionId == 0)
            {
                foreach (var img in imgSources)
                {
                    img.color = colorSet;
                }
            }
            else
            {
                foreach (var img in imgSources)
                {
                    img.color = colorDefault;
                }
            }
        }
    }
}
