using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestructedDestroyGameObject : MonoBehaviour, IDestructable
{
    public void OnDestruction(GameObject attacker)
    {
        if(gameObject.tag == "Player")
        {
            SceneManager.LoadScene(0);
        }
        Destroy(gameObject);
    }
}
