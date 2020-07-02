using BowArrow;
using UnityEngine;

public class OculusInput : MonoBehaviour
{
    public Bow m_Bow = null;
    public GameObject m_OppositeController = null;
    public OVRInput.Controller m_Controller = OVRInput.Controller.None;

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, m_Controller)) m_Bow.Pull(m_OppositeController.transform);

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, m_Controller))
        {
            m_Bow.Release();
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }
            
    }
}