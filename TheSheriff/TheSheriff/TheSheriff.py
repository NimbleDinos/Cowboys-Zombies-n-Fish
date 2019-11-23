import datetime
import numpy as np

import discord
import discord.ext
from discord.ext.commands import Bot
from discord.ext import commands

import UserInfo

bot_prefix = "?"
client = commands.Bot(command_prefix=bot_prefix)
client.remove_command("help")

playerList = []
unitList = ['fisher','attacker','defender','builder']
placeList = ['near','mid','far']

@client.event
async def on_ready():
	print("The Sheriff is online")
	print("Name: The Sheriff")
	print("TD: {}".format(client.user.id))

@client.command()
async def joingame(ctx):
	userID = ctx.message.author.id
	userName = ctx.message.author.name
	returned = await UserInfo.newPlayer(userID, userName, playerList)
	
	if returned == 0:
	    await ctx.send(userName +" you are already in the game!")
	elif returned == 1:
		playerList.append([userID,0])
		await ctx.send(userName + " you have entered the game!")

@client.command()
async def action(ctx, unit, place):
	userID = ctx.message.author.id
	userName = ctx.message.author.name

	if unit not in unitList:
	    await ctx.send(userName + ", that unit does not exist")
	if place not in placeList:
		await ctx.send(userName + ", that place does not exist")
	else:
		returned = await UserInfo.playerCommand(userID, playerList)	

		if returned == 0:
			await ctx.send(userName + " you have already sent a command this turn")
		elif returned == 1:
			for item in playerList:
				if item == [userID, 0]:
					item = [userID, 1]
					playerList.append(item)
			await ctx.send(userName + ", your command has been accepted")	


@client.command()
async def test(ctx):

	await ctx.send("Test Command")

@client.event
async def on_message(message):
	if message.author.bot:
		return

	if "texttest" in message.content.lower():
		await message.channel.send("on_message test")

	await client.process_commands(message)

@client.command()
async def howdy(ctx):

	await ctx.send("Howdy Partner!")

file = open("token.txt", "r")
token = str(file.read())
file.close()
client.run(token)