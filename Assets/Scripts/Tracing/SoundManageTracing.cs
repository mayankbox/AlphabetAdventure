using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManageTracing : MonoBehaviour
{
	[Header("Setting")]
	public Toggle SoundToggle;

	[SerializeField] AudioSource BgClip;

    private void Start()
    {
		SetSoundToggle();
    }
    public void SetSoundToggle()
	{
		print(" ==Sound== 000    >>>> " + GameConfige.Sound);
		if (GameConfige.Sound == 0)
		{
			SoundToggle.isOn = true;
			BgClip.Pause();

		}
		else
		{
			SoundToggle.isOn = false;
			BgClip.Play();

		}
	}

	public void SetSoundToogle(Toggle toggle)
	{
		if (toggle.isOn)
		{
			GameConfige.Sound = 0;
			BgClip.Pause();

		}
		else
		{
			GameConfige.Sound = 1;
			BgClip.Play();

		}
		print(" ==Sound== >>>> " + GameConfige.Sound);
	}
}
