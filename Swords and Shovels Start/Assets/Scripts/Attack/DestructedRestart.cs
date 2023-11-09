using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestructedRestart : MonoBehaviour, IDestructable
{
    public void OnDestruction(GameObject attacker)
    {
        SceneManager.LoadScene(0);
    }
}
