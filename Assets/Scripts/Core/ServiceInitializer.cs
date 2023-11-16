using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace Trellcko.DefenseFromMonster.Core
{
    public class ServiceInitializer : MonoBehaviour
    {
        private async void Start()
        {
            if (UnityServices.State == ServicesInitializationState.Uninitialized)
            {
                await UnityServices.InitializeAsync();
            }

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                AuthenticationService.Instance.SignedIn += DebugSignIn;
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }

        private void DebugSignIn()
        {
            AuthenticationService.Instance.SignedIn -= DebugSignIn;
            Debug.Log("Signed In " + AuthenticationService.Instance.PlayerId);
        }
    }
}
