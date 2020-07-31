using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.EventMaster
{
    class StartRoom02EventTrigger : MonoBehaviour
    {
        private GameObject _RoomMaster;

        void Start()
        {
            _RoomMaster = GameObject.FindWithTag("RoomMaster");
        }

        private void OnTriggerExit(Collider col)
        {
            if (col.tag == "Player")
            {
                _RoomMaster.gameObject.GetComponent<Events>().SetTriggered(true);
                this.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
