using UnityEngine;
using XboxCtrlrInput;
using System.Collections;

// Defines the variable information for each class.
public class ClassInfo : MonoBehaviour {

    [Header("Class Info")]
    public XboxController assignedController;
    public string className;

    [Header("Class Properties")]
    public float velocity;
    public float jumpForce;
    public int attackDamage;

    [Header("Effects")]
    public bool isSlowdown;
    public bool isStuned;

}
