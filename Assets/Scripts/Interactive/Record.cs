using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Radio.ContactItem
{
    public class Record : MonoBehaviour, IClick
    {

        
        public void EnterLeftClick()
        {
            Debug.Log("Record Start");
            RecordSection.Instance.ShowRecord();
            ValueManager.Instance.RightClickEvent.AddListener(RecordSection.Instance.CloseRecord);
        }

        public void EnterRightClick()
        {
            
        }

        public void ExitLeftClick()
        {
            //ValueManager.Instance.RightClickEvent.RemoveListener(RecordSection.Instance.CloseRecord);
        }

        public void ExitRightClick()
        {
            
        }
    }
}