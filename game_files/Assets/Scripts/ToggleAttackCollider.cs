using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.EventMaster;
using UnityEngine;

namespace Assets.Scripts
{
    class ToggleAttackCollider : MonoBehaviour
    {
        private GameObject _object;
        private DetectAttackCollision col;

        // Used in animation events
        public void ToggleCollider(string colliderName)
        {
            //_object = this.gameObject;
            _object = this.gameObject.transform.Find(colliderName).gameObject;
            col = _object.transform.GetComponent<DetectAttackCollision>();

            if (col.GetColliderValue())
                col.ChangeColliderValue(false);

            else if (!col.GetColliderValue())
                col.ChangeColliderValue(true);
        }

        public void FalseCollider(string colliderName)
        {
            _object = this.gameObject.transform.Find(colliderName).gameObject;
            col = _object.transform.GetComponent<DetectAttackCollision>();

            col.ChangeColliderValue(false);
        }
    }
}