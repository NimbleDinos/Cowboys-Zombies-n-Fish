import discord
from discord.ext.commands import Bot
from discord.ext import commands

bot_prefix = "?"
client = commands.Bot(command_prefix=bot_prefix)
client.remove_command("help")

@client.event
async def on_ready():
	print("The Sheriff is online")
	print("Name: The Sheriff")
	print("TD: {}".format(client.user.id))

@client.event
async def on_message(message):
	if message.author.bot:
		return

	if "texttest" in message.content.lower():
		await message.channel.send("on_message test")

	await client.process_commands(message)

@client.command()
async def test(ctx):
	await ctx.send("Test Command")

@client.command()
async def howdy(ctx):
	await ctx.send("Howdy Partner!")

client.run("")
