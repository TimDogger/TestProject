using System.Text.RegularExpressions;
using Mirror;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StartScreenUI : MonoBehaviour
    {
        [SerializeField]
        private NetworkManager networkManager;
    
        [SerializeField]
        private TMP_InputField ipAddressInputField;
    
        public void HostLobby()
        {
            networkManager.StartHost();
        }
    
        public void JoinLobby()
        {
            networkManager.networkAddress = ipAddressInputField.text;
            networkManager.StartClient();
        }

        public void ValidateIpAdress()
        {
            string ip = ipAddressInputField.text;
            string pattern = @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
        
            if (Regex.IsMatch(ip, pattern)) 
            {
                Debug.Log("IP address is valid.");
            } 
            else 
            {
                Debug.Log("IP address is not valid.");
            }
        }
    }
}
