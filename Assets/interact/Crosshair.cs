using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
	[SerializeField] float detectionRange = 0.5f; // 감지 범위
	[SerializeField] Image crosshair;
	[SerializeField] float fadeDuration = 0.5f; // 페이드인/아웃 시간

	private RaycastHit hit;
	private Ray ray;

	private Coroutine fadeCoroutine;

	void Start()
	{
		// 처음에 조준선 비활성화
		SetCrosshairAlpha(0f);
	}

	void Update()
	{
		ray = Camera.main.ViewportPointToRay(new Vector3(detectionRange, detectionRange, 0));
		bool hitSomething = Physics.Raycast(ray, out hit, 1f);

		if (hitSomething && (
			hit.collider.gameObject.tag == "interactable" ||
			hit.collider.gameObject.tag == "obtainable"
			))
		{
			if (fadeCoroutine != null)
			{
				StopCoroutine(fadeCoroutine);
			}
			fadeCoroutine = StartCoroutine(FadeCrosshair(1f));
		} else {
			// Interactable 오브젝트가 없으면 조준선 페이드아웃
			if (fadeCoroutine != null) {
				StopCoroutine(fadeCoroutine);
			}
			fadeCoroutine = StartCoroutine(FadeCrosshair(0f));
		}
	}

	void SetCrosshairAlpha(float alpha)
	{
		Color color = crosshair.color;
		color.a = alpha;
		crosshair.color = color;
	}

	IEnumerator FadeCrosshair(float targetAlpha)
	{
		float startAlpha = crosshair.color.a;
		float elapsedTime = 0f;

		while (elapsedTime < fadeDuration)
		{
			elapsedTime += Time.deltaTime;
			float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
			SetCrosshairAlpha(alpha);
			yield return null;
		}

		SetCrosshairAlpha(targetAlpha);
	}
}
