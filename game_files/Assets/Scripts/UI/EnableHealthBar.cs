using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableHealthBar : MonoBehaviour
{
    [SerializeField] private GameObject _healthBarUI;
    [SerializeField] private BossInteraction _boss;

    // Update is called once per frame
    void Update()
    {
        if (_boss.ReturnActiveState())
            _healthBarUI.SetActive(true);
    }
}
