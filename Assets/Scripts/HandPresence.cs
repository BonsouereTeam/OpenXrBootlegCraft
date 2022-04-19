using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
	public InputDeviceCharacteristics controllerCharacteristics;
	public GameObject handModelPrefab;

	private InputDevice targetDevice;
	private GameObject spawnedHandModel;
	private Animator handAnimator;

	// Start is called before the first frame update
	void Start()
	{
		TryInitialize();
	}

	void TryInitialize()
	{
		//Permet d'avoir la liste des controleurs.
		List<InputDevice> devices = new List<InputDevice>();

		//Permet d'aller chercher les contrôleurs
		InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

		//Vérifie si un controleur est initialiser
		if (devices.Count > 0)
		{
			targetDevice = devices[0];
			//Permet d'instantier un préfab.
			spawnedHandModel = Instantiate(handModelPrefab, transform);
			//Permet de récupérer l'Animator de l'objet qui vient d'être instantier
			handAnimator = spawnedHandModel.GetComponent<Animator>();
		}
	}
	//Fonction qui permet de faire animer les mains du joueur
	void UpdateHandAnimation()
	{
		//Fonction qui permet de récupérer la pression qui est mis sur le Trigger de la manette
		if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
		{
			//Permet d'animer la main selon la pression détecter
			handAnimator.SetFloat("Trigger", triggerValue);
		}
		else
		{
			handAnimator.SetFloat("Trigger", 0);
		}
		//Fonction qui permet de récupérer la pression qui est mis sur le Grip de la manette 
		if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
		{
			handAnimator.SetFloat("Grip", gripValue);
		}
		else
		{
			handAnimator.SetFloat("Grip", 0);
		}
	}

	// Update is called once per frame
	void Update()
	{
		//Véririe si un controlleur est initialiser.
		if (!targetDevice.isValid)
		{
			TryInitialize();
        }
        else
        {
			UpdateHandAnimation();
		}
	}
}