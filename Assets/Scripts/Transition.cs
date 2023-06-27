using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Animator animator;
    [SerializeField] private float x, y;

    public float X { get => x; set => x = value; }
    public float Y { get => y; set => y = value; }
    public string SceneName { get => sceneName; set => sceneName = value; }

    public void StartAnimation(int state)
    {
        animator.SetInteger("state", state);
    }
    public void StartTransition()
    {
        SceneManager.LoadScene(sceneName);
        Transform transformPlayer = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        transformPlayer.position = new Vector3(x, y);
    }
}
