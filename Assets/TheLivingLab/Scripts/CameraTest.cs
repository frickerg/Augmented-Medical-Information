using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class CameraTest : MonoBehaviour
{
    
    GameObject dialog = null;
    // War void Start
    public void CheckCamera()
    {
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
            dialog = new GameObject();
            
        }
#endif
    }

    void Ongui()
    {
#if platform_android
            if (!permission.hasuserauthorizedpermission(permission.camera))
            {
                // the user denied permission to use the camera.
                // display a message explaining why you need it with yes/no buttons.
                // if the user says yes then present the request again
                // display a dialog here.
                dialog.addcomponent<permissionsrationaledialog>();
                return;
            }
            else if (dialog != null)
            {
                destroy(dialog);
            }
#endif

        // now you can do things with the camera
    }
}