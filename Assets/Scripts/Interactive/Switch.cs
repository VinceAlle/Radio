using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Radio.ContactItem
{
    public class Switch : MonoBehaviour, IClick
    {

        HighlightableObject highlightable;

        private Transform slider;
        private float on = 0.3272f;
        private float off;
        private bool isOff = true;


        private SpriteRenderer renderer;
        private Color orginalColor;
        public Color changeColor;

        public GameObject Scene;
        public GameObject SceneLight;
        

        private void Start()
        {
            highlightable = GetComponentInChildren<HighlightableObject>();
            var allTrans = GetComponentsInChildren<Transform>();
            
            renderer = GetComponentInChildren<SpriteRenderer>();
            orginalColor = renderer.color;
            

            for (int i = 0; i < allTrans.Length; i++)
            {
                Debug.Log(allTrans[i].name);
            }
            slider = allTrans[2];
            off = slider.localPosition.y;
        }
        bool start = true;
        IEnumerator CloseLight()
        {
            yield return new WaitForSeconds(2f);
            SceneLight.SetActive(false);
            if (start)
            {
                ValueManager.Instance.NextLog();
                start = false;
            }
        }

        public void EnterLeftClick()
        {
            highlightable.ConstantOn();
            if (isOff)
            {
                slider.localPosition = new Vector3(slider.localPosition.x, on, slider.localPosition.z);
                renderer.color = changeColor;
                isOff = !isOff;
                Scene.SetActive(true);
                StartCoroutine(CloseLight());
            }
            else
            {
                slider.localPosition = new Vector3(slider.localPosition.x, off, slider.localPosition.z);
                renderer.color = orginalColor;
                isOff = !isOff;
            }
        }

        public void ExitLeftClick()
        {
            highlightable.ConstantOff();
        }

        public void EnterRightClick()
        {
            
        }

        public void ExitRightClick()
        {
            
        }
    }
}