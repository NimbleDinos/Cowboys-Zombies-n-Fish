using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

public class CowboyInterpreterHelper : MonoBehaviour
{
    public SpawnPlayer spawnPlayer;

    // Start is called before the first frame update
    async void Update()
    {
        var command = await GetCommand();

        if (command.action == "join")
        {
            spawnPlayer.SpawnNewPlayer(command.UserID, command.userName);
        }

		if (command.action == "chooseClass")
		{
			spawnPlayer.ChangeRole(command.UserID, command.clas);
		}

		if (command.action == "move")
		{
            Debug.Log("Moving");
			spawnPlayer.MoveUnit(command.UserID, command.x, command.y);
		}

		if (command.action == "action")
		{
			if (command.builder == null)
			{
				spawnPlayer.PerformAction(command.UserID);
			}
			else 
			{
                string[] bits = command.builder.Split('-');
                string buildable = bits[0];
                string pos = bits[1];
                spawnPlayer.PerformAction(command.UserID, buildable, pos);
			}
			
		}

	}

    // Update is called once per frame
    async Task<UserCommand> GetCommand()
    {
        var response = await new WWW("http://localhost:8081/getCommand");
        if (!string.IsNullOrEmpty(response.error))
        {
            throw new Exception();
        }
        var json = response.text;
        //Debug.Log(json);
        var command = JsonConvert.DeserializeObject<UserCommand>(json);
        //Debug.Log(command.UserID);

        return command;
    }
}
