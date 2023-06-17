using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Radio
{
    public class LeftButton : MonoBehaviour, IClick
    {

        HighlightableObject highlightable;

        private void Start()
        {
            highlightable = GetComponent<HighlightableObject>();
        }
        public void EnterLeftClick()
        {
            highlightable.ConstantOn();
        }

        public void EnterRightClick()
        {
            
        }

        public void ExitLeftClick()
        {
            highlightable.ConstantOff();
        }

        public void ExitRightClick()
        {
            
        }
    }
}