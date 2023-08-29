using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using System.Runtime.InteropServices;

[DefaultExecutionOrder(100)]
public class CheckRemoteKeys : MonoBehaviour
{
    public static CheckRemoteKeys instance;

    [SerializeField] bool okConnection = false;
    public bool OkConnection { get => okConnection; set => okConnection = value; }


    #region Remote Keys


    bool renewLicense = false;
    public bool RenewLicense { get => renewLicense; set => renewLicense = value; }
    #endregion


    public struct userAttributes { }
    public struct appAttributes { }



    async Task InitializeRemoteConfigAsync()
    {
        // initialize handlers for unity game services
        await UnityServices.InitializeAsync();

        // remote config requires authentication for managing environment information
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            //Comento el SingIn para ir más rápido
            //await AuthenticationService.Instance.SignInAnonymouslyAsync();

            okConnection = true;

            RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
            RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
        }
    }

    async Task Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //initialize Unity's authentication and core services, however check for internet connection
        // in order to fail gracefully without throwing exception if connection does not exist
        if (Unity.Services.RemoteConfig.Utilities.CheckForInternetConnection())
        {
            await InitializeRemoteConfigAsync();
        }
        else
        {
            Debug.Log("Fail Remote Config Connection");

            okConnection = false;
        }
    }

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        Debug.Log("RemoteConfigService.Instance.appConfig fetched: " + RemoteConfigService.Instance.appConfig.config.ToString());

        renewLicense = RemoteConfigService.Instance.appConfig.GetBool("AllowRenewLicense");
    }

}