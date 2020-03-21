using UnityEngine;
using UnityEngine.SceneManagement;

public class MakeScreenshotByMouseClick : MonoBehaviour
{
	public Camera mainCamera;

	int counter = 1;

	private void Update()
	{
		if (Input.GetKeyDown("r"))
		{
			ScreenCapture.CaptureScreenshot("Assets/Sreenshot" + counter.ToString("00") + "_" + mainCamera.pixelWidth + "x" + mainCamera.pixelHeight + "_" + "_SceneID"+ SceneManager.GetActiveScene().name + ".png");
			counter++;
		}
	}
}
