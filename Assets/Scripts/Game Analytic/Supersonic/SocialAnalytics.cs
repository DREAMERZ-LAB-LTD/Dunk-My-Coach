using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialAnalytics : MonoBehaviour
{
    private void Awake()
    {
        InitilizeFaceook();
    }
 

    private void InitilizeFaceook()
    {

        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                {
                    FB.ActivateApp();
#if UNITY_EDITOR
                    Debug.Log("IsInitialized");
#endif
                }
                else
                {
#if UNITY_EDITOR
                    Debug.Log("Couldn't initialize");
#endif
                }
            },
            isGameShown =>
            {
                if (!isGameShown)
                {
#if UNITY_EDITOR
                    Debug.Log("IS not Game Shown");
#endif
                }
                else
                {
#if UNITY_EDITOR
                    Debug.Log("IS Game Shown");
#endif
                }
            });
        }
        else
        {
            FB.ActivateApp();
        }

    }

    /// <summary>
    /// login facebook user 
    /// </summary>
    public void OnClickFacebookLogin()
    {

        IEnumerable<string> Permissions = new List<string>() { "public_profile", "email"/*, "user_friends" */};
        FB.LogInWithReadPermissions(Permissions, Auth_Callback);

        void Auth_Callback(ILoginResult result)
        {
            if (FB.IsLoggedIn)
            {
                FB.API("/me?fields=id,name,email", HttpMethod.GET, GetUserInfo);//sent request to faceook for retrive user data

            }
            else
            {
             
            }
        }

        void GetUserInfo(IResult userInfo)
        {

            if (userInfo.Error != null)
            {
#if UNITY_EDITOR
                Debug.Log("an Error");
#endif
                return;
            }
        
            //string name = userInfo.ResultDictionary["name"].ToString();
            //string id = userInfo.ResultDictionary["id"].ToString();
            //string email = userInfo.ResultDictionary["email"].ToString();
            //string token = AccessToken.CurrentAccessToken.ToString(); // Get access token from faceook

        }
    }

}
