import datetime
import numpy as np

import discord
import discord.ext
from discord.ext.commands import Bot
from discord.ext import commands

import UserInfo
import requests

bot_prefix = "?"
client = commands.Bot(command_prefix=bot_prefix)
client.remove_command("help")

playerList = []
unitList = ['fisher', 'attacker', 'defender', 'builder']
placeList = ['near', 'mid', 'far']
acceptedClasses = ['fisher', 'attacker', 'builder']

baseUrl = "http://localhost:8081/"


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
		await ctx.send(userName + " you are already in the game!")
	elif returned == 1:
		try:
			playerList.append([userID, 0])
			joinGameUrl = baseUrl + "command?action=join&userId={0}".format(userID)
			response = requests.get(joinGameUrl)
			if (response.status_code == 200):
				await ctx.send(userName + " you have entered the game!")
		except:
			await ctx.send("Whoa there {0}! Somethin' ain't right here".format(userName))

@client.command()
async def chooseClass(ctx, clas):
	userID = ctx.message.author.id
	userName = ctx.message.author.name

	if clas in acceptedClasses:
		try:
			chooseClassGameurl = baseUrl + "command?action=chooseClass&userId={0}&class={1}".format(userID, clas)
			response = requests.get(chooseClassGameurl)
			if (response.status_code == 200):
				await ctx.send("Good luck out there, {0}!".format(userName))
		except:
			await ctx.send("Whoa there {0}! Somethin' ain't right here".format(userName))
	else:
		await ctx.send("Sorry partner, I ain't gotta clue what a {0} is".format(clas))

@client.command()
async def move(ctx, x, y):
	userID = ctx.message.author.id
	userName = ctx.message.author.name

	MAX_X = 100
	MAX_Y = 50
	MIN_XY = 0

	if (MIN_XY <= int(x) <= MAX_X) & (MIN_XY <= int(y) <= MAX_Y):
		try:
			moveUrl = baseUrl + "command?action=move&userId={0}&x={1}&y={2}".format(userID, x, y)
			response = requests.get(moveUrl)
			if (response.status_code == 200):
				await ctx.send("See ya soon cowboy")
		except:
			await ctx.send("Whoa there {0}! Somethin' ain't right here".format(userName))
	else:
		await ctx.send("I'm afraid ({0}, {1}) is out of bounds {2}".format(x, y, userName))

@client.command()
async def action(ctx):
	userID = ctx.message.author.id
	userName = ctx.message.author.name

	try:
		playerList.append([userID, 0])
		joinGameUrl = baseUrl + "command?action=action&userId={0}".format(userID)
		response = requests.get(joinGameUrl)
		if (response.status_code == 200):
			await ctx.send(userName + " go do your thing partner!")
	except:
		await ctx.send("Whoa there {0}! Somethin' ain't right here".format(userName))

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
