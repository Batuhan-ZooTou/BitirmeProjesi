using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Extensions;
using Cinemachine;
namespace _Game.Scripts.Managers
{
    public class CameraManager : Singleton<CameraManager>
    {
        public CinemachineBrain brain;
        public CinemachineVirtualCamera defaultCamera;
        public CinemachineBlenderSettings defaultBlendSettings;
        public CinemachineBlenderSettings dialogueBlendSettings;

        public void SwitchCamera(CinemachineVirtualCamera cameraToSwitch,bool isDialogue)
        {
            brain.ActiveVirtualCamera.Priority =0;
            cameraToSwitch.Priority = 10;
            if (isDialogue)
            {
                brain.m_CustomBlends = dialogueBlendSettings;
            }
            else
            {
                brain.m_CustomBlends = defaultBlendSettings;
            }
        }
        public void SwitchToDefaultCam()
        {
            SwitchCamera(defaultCamera, false);
        }

        
    }
}