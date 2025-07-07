using System;
using UnityEngine;

namespace _01.Code.Test
{
    public class Test13 : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.PageUp))
            {
                Time.timeScale = 8;
            }
            if (Input.GetKeyDown(KeyCode.PageDown))
            {
                Time.timeScale = 1;
            }
        }
    }
}