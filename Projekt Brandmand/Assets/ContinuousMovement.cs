using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    public XRNode inputSource;
    private Vector2 inputAxis;
    private CharacterController character;
    private XRRig rig;

    [SerializeField]
    private float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }
    private void FixedUpdate()
    {
        Quaternion headYaw = Quaternion.Euler(x: 0, rig.cameraGameObject.transform.eulerAngles.y, z: 0); 
        Vector3 direction = headYaw * new Vector3(inputAxis.x, y: 0, inputAxis.y);
        character.Move(direction * speed * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }
}
