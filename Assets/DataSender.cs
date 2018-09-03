using UnityEngine;

[DefaultExecutionOrder(1000)]
public class DataSender : MonoBehaviour
{
    Panel[] panels;

	private void Start()
	{
        panels = FindObjectsOfType<Panel>();
        Debug.Log(panels.Length + " panels");
	}

	private void FixedUpdate()
    {
        foreach (var p in panels)
        {
            switch (p.GetTouch())
            {
                case TouchResponse.Entered:
                    GroundSwiper.I.QueueData(KeyMode.Down, p.KeyInfo);
                    break;
                case TouchResponse.Leaved:
                    GroundSwiper.I.QueueData(KeyMode.Up, p.KeyInfo);
                    break;
            }
        }
        GroundSwiper.I.SendData();
    }

	private void OnDestroy()
	{
        GroundSwiper.I.Close();
	}
}