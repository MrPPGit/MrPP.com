
using UnityEngine;

#if !UNITY_EDITOR && UNITY_WSA
using Windows.Networking;
using Windows.Networking.Connectivity;
#else

using System.Net.NetworkInformation;
using System.Net.Sockets;
#endif



namespace MrPP {
    public class PlatformInfo : GDGeek.Singleton<PlatformInfo> {
        public enum Type {
            HoloLens,
            Desktop,
            Mobile,
        }

        [SerializeField]
        private bool _auto = false;
        [SerializeField]
        private Type _type = Type.HoloLens;

        public Type type {

            get {

                if (_auto)
                {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
                    _type = Type.Desktop;
#endif
#if !UNITY_EDITOR && UNITY_IOS
                _type = Type.Mobile;
#endif


#if !UNITY_EDITOR && UNITY_WSA
                
                _type = Type.HoloLens;
#endif

                }
                return _type;
            }
        }

        private string localIp_;
        private string localComputerName_;
        private void Awake()
        {
            if (_auto) {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN 
                _type = Type.Desktop;
#endif
#if !UNITY_EDITOR && UNITY_IOS
                _type = Type.Mobile;
#endif


#if !UNITY_EDITOR && UNITY_WSA
                
                _type = Type.HoloLens;
#endif

            }
        }
        private void Start()
        {
            Debug.Log(LocalIp);   
        }
#if !UNITY_EDITOR && UNITY_WSA
        public static string GetAddressIP()
        {
        
            string AddressIP = string.Empty;
            foreach (HostName hostName in NetworkInformation.GetHostNames())
            {
                if (hostName.DisplayName.Split(".".ToCharArray()).Length == 4)
                {
                    Debug.Log("Local IP " + hostName.DisplayName);
                    AddressIP =  hostName.DisplayName;
                }
            }
            return AddressIP;
        }
#else

        public static string GetAddressIP()
        {
            string AddressIP = string.Empty;
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            for (int i = 0; i < adapters.Length; i++)
            {
                if (adapters[i].Supports(NetworkInterfaceComponent.IPv4))
                {
                    UnicastIPAddressInformationCollection uniCast = adapters[i].GetIPProperties().UnicastAddresses;
                    if (uniCast.Count > 0)
                    {
                        for (int j = 0; j < uniCast.Count; j++)
                        {
                            //得到IPv4的地址。 AddressFamily.InterNetwork指的是IPv4
                            //Debug.Log(uniCast[j].Address.ToString());
                            //Debug.Log(uniCast[j].Address.AddressFamily.ToString());
                            if (uniCast[j].Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                AddressIP = uniCast[j].Address.ToString();
                            }
                        }
                    }
                }
            }

            return AddressIP;
        }
#endif
        public static string LocalIp {

            get {
              
                if (string.IsNullOrEmpty(PlatformInfo.Instance.localIp_)) {

                   PlatformInfo.Instance.localIp_ = GetAddressIP();
                }
                return PlatformInfo.Instance.localIp_;
            }
        }
       
        public static string LocalComputerName
        {

            get
            {
                if (string.IsNullOrEmpty(PlatformInfo.Instance.localComputerName_))
                {
#if !UNITY_EDITOR && UNITY_WSA
                    foreach (HostName hostName in NetworkInformation.GetHostNames())
                    {
                        if (hostName.Type == HostNameType.DomainName)
                        {

                            Debug.Log("My name is " + hostName.DisplayName);
                            PlatformInfo.Instance.localComputerName_ = hostName.DisplayName;
                        }
                    }
                    if (string.IsNullOrEmpty(PlatformInfo.Instance.localComputerName_))
                    {
                        PlatformInfo.Instance.localComputerName_ = "NotSureWhatMyNameIs";
                    }
#else
                    PlatformInfo.Instance.localComputerName_ =  System.Environment.MachineName;
#endif
                }

                return PlatformInfo.Instance.localComputerName_;

                

            }
        }


    }
}