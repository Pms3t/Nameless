using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Boo.Lang;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillAndRespawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other);
        SceneManager.LoadScene("Level01");
    }
}
