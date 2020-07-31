using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class DestroyOnCollide : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                // Spawn effect when hit
                //Instantiate();
                Destroy(this.gameObject);
            }
        }
    }
}
