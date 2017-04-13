using UnityEngine;
using UnityEngine.UI;
using System;

public class LobbyRoom : MonoBehaviour 
{
	public Text statusText;
	public Text nameText;
	public Text playersText;
	public Text mapText;
	public Text pingText;
	public Image background;
	public Color evenColor, oddColor;
		
	LobbyModel.Room m_room;
	public void Set(int position, LobbyModel.Room r)
    {
        m_room = r;
        background.color = position % 2 == 0 ? evenColor : oddColor;

        nameText.text = r.name;
        mapText.text = r.map;

        r.state.RegisterObserver(UpdateState);
        r.players.RegisterObserver(UpdatePlayers);
        r.ping.RegisterObserver(UpdatePing);
    }

    void UpdateState(LobbyModel.TState s)
    {
        statusText.text = s.ToString();
    }
    void UpdatePlayers(int p)
    {
        playersText.text = string.Format("{0}/{1}", p, m_room.maxPlayers);
    }
    void UpdatePing(int p)
    {
        pingText.text = p.ToString();
    }

    void OnDestroy()
	{
		if (m_room != null) 
		{
			m_room.state.RemoveObserver (UpdateState);
			m_room.players.RemoveObserver (UpdatePlayers);
			m_room.ping.RemoveObserver (UpdatePing);
		}
	}
}
