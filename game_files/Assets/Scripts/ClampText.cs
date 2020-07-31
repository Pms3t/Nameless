using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class ClampText : MonoBehaviour
    {
        public Text label;

        void Update()
        {
            Vector3 textPos = Camera.main.WorldToScreenPoint(this.transform.position);
            label.transform.position = textPos;
        }
    }
}
