using UnityEngine;
public class MaskItem: IvenItem
{
	public Sprite mask;
	void Start()
	{
		Foreground.sprite = mask;
	}
}