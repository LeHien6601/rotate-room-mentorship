using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualScript : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;
    public void ChangeStateToJumping()
    {
        playerAnim.SetFloat("jumpX", 0);
        playerAnim.SetFloat("jumpY", 1);
    }
}
