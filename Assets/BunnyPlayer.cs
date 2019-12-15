using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
[RequireComponent(typeof(BowController))]
public class BunnyPlayer : Life
{
    public float moveSpeed = 5;
    PlayerController controller;
    BowController bowController;

    public Animator anim;
    Crosshairs crosshairs;
    Camera viewCamera;

    protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerController>();
        bowController = GetComponent<BowController>();
        crosshairs = GameObject.FindGameObjectWithTag("Crosshairs").GetComponent<Crosshairs>();
        viewCamera = Camera.main;
    }

    protected override void Update()
    {
        //Movement
        base.Update();
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0)
        {
            anim.SetBool("Walk", true);
        } else
        {
            anim.SetBool("Walk", false);
        }

        //Look Input //Will return a ray from the camera to mouse position.
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero * bowController.BowHeight);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            controller.LookAt(point);
            crosshairs.transform.position = point;
            crosshairs.DetectTargets(ray);
        }

        //Weapon
        if (Input.GetMouseButton(0)) //Mouse Button 0 means left mouse button
        {
            bowController.Shoot();
        }
    }
}
