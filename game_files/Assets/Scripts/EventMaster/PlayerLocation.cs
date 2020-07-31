using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.EventMaster
{
    class PlayerLocation : MonoBehaviour
    {
        private GameObject _playerInstance;

        void Start()
        {
            _playerInstance = GameObject.FindWithTag("Player");
        }

        public Transform GetLocation()
        {
            var location = _playerInstance.transform;
            return location;
        }
    }
}
