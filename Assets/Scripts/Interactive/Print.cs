using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Radio
{
    public class Print : MonoBehaviour, IClick
    {

        
        public void EnterLeftClick()
        {
            Debug.Log("Print Start");
            RecordSection.Instance.CreateLeftPage();
            ValueManager.Instance.RightClickEvent.AddListener(RecordSection.Instance.CloseRecord);
        }

        public void EnterRightClick()
        {
            
        }

        public void ExitLeftClick()
        {
            ValueManager.Instance.RightClickEvent.RemoveListener(RecordSection.Instance.CloseRecord);
        }

        public void ExitRightClick()
        {
            
        }
    }
}