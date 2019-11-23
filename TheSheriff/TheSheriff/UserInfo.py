
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

async def infoUpdate(ctx):
	await ctx.send("")
	return 0

async def help():
	return 0