using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPSController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField]
    private float normalSensitivity;
    [SerializeField]
    private float aimSensitivity;
    [SerializeField]
    private Transform debugTransform;
    [SerializeField]
    private Transform pfBulletProjectile;
    [SerializeField]
    private Transform spawnBulletProjectile;

    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();

   

    private ThirdPersonController ThirdPersonController;
    private StarterAssetsInputs StarterAssetsInputs;

    public void Awake()
    {
        ThirdPersonController = GetComponent<ThirdPersonController>();
        StarterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }
    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if (StarterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            ThirdPersonController.SetSensitivity(aimSensitivity);
            ThirdPersonController.SetRotateOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            ThirdPersonController.SetSensitivity(normalSensitivity);
            ThirdPersonController.SetRotateOnMove(true);
        }
        if(StarterAssetsInputs.shoot)
        {
            
            Vector3 aimDir = (mouseWorldPosition - spawnBulletProjectile.position).normalized;
            Instantiate(pfBulletProjectile, spawnBulletProjectile.position, Quaternion.LookRotation(aimDir,Vector3.up));
            StarterAssetsInputs.shoot = false;
        }


    }
}
