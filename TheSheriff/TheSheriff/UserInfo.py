import datetime

async def newPlayer(userID, userName, playerList):
	
	if userID in playerList:
		return 1
	else:
		return 0
		

async def playerCommand(userID, playerList):
	
	if [userID, 1] in playerList:
		return 0
	else:
		return 1

async def help():
	return 0