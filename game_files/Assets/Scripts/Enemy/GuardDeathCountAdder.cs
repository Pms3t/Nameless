using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    class GuardDeathCountAdder : MonoBehaviour
    {
        [SerializeField] private GameObject _roomMaster;

        void Update()
        {
            if (GetComponent<CharacterStats>().GetCurrentHealth() <= 0)
            {
                _roomMaster.GetComponent<BossEvent>()._guardCount++;
                enabled = false;
            }
        }
    }
}
