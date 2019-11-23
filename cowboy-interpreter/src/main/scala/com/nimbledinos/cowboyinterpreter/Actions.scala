package com.nimbledinos.cowboyinterpreter

import cats.syntax.either._

case class Action(UserID: Long, action: String, clas: Option[String] = None, x: Int = -1, y: Int = -1)

object Actions {

  final case class CommandCreationException(message: String) extends Exception

  val MAX_X: Int  = 100
  val MAX_Y: Int  = 50
  val MIN_XY: Int = 0

  def getActionFromValue(actionString: String, userId: Long, clas: Option[String], x: Option[Int], y: Option[Int]): Either[CommandCreationException, Action] =
    actionString match {
      case "join"        ⇒ Right(Action(userId, actionString))
      case "chooseClass" ⇒ validateChooseClass(userId, actionString, clas)
      case "move"        ⇒ validateMovement(userId, actionString, x, y)
      case "action"      ⇒ Right(Action(userId, actionString))
    }

  def validateChooseClass(userID: Long, action: String, clas: Option[String]): Either[CommandCreationException, Action] =
    clas
      .toRight("Class was not present")
      .flatMap { classValue ⇒
        val classOrError = Class.fromValue(classValue)
        classOrError.map(validatedClass ⇒ Action(userID, action, clas = Some(validatedClass)))
      }
      .leftMap(CommandCreationException)

  def validateMovement(userID: Long, action: String, x: Option[Int], y: Option[Int]): Either[CommandCreationException, Action] = {
    def validateCoord(value: Int, isX: Boolean): Option[Int] =
      if (isX)
        if (MIN_XY to MAX_X contains (value)) Some(value)
        else None
      else if (MIN_XY to MAX_Y contains (value)) Some(value)
      else None

    for {
      xCoord     ← x
      yCoord     ← y
      validatedX ← validateCoord(xCoord, isX = true)
      validatedY ← validateCoord(yCoord, isX = false)
    } yield Action(userID, action, x = validatedX, y = validatedY)
  }.toRight(CommandCreationException("An error occurred when creating the movement command"))
}
