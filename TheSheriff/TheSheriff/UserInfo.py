import datetime

async def newPlayer(userID, userName, playerList):
	
	if [userID, 0] in playerList:
		return 0
	else:
		return 1
		

async def playerCommand(userID, playerList):
	
	if [userID, 1] in playerList:
	    return 0
	else:
		return 1

async def infoUpdate():
	seconds = datetime.datetime.now().second
	if seconds % 15 == 0:
		return 0
	elif seconds % 60 == 0:
		return 1
	else:
		return 2

async def help():
	return 0