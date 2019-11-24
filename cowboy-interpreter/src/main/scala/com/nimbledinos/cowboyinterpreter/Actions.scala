package com.nimbledinos.cowboyinterpreter

import cats.syntax.either._

case class Action(UserID: Long, userName: String, action: String, clas: Option[String] = None, x: Int = -1, y: Int = -1, builder: Option[String] = None)

object Actions {

  final case class CommandCreationException(message: String) extends Exception

  val MAX_X: Int  = 100
  val MAX_Y: Int  = 50
  val MIN_XY: Int = 0

  def getActionFromValue(actionString: String,
                         userId: Long,
                         userName: String,
                         clas: Option[String],
                         x: Option[Int],
                         y: Option[Int],
                         builder: Option[String]): Either[CommandCreationException, Action] =
    actionString match {
      case "join"        ⇒ Right(Action(userId, userName, actionString))
      case "chooseClass" ⇒ validateChooseClass(userId, userName, actionString, clas)
      case "move"        ⇒ validateMovement(userId, userName, actionString, x, y)
      case "action"      ⇒ Right(Action(userId, userName, actionString, builder))
    }

  def validateChooseClass(userID: Long, userName: String, action: String, clas: Option[String]): Either[CommandCreationException, Action] =
    clas
      .toRight("Class was not present")
      .flatMap { classValue ⇒
        val classOrError = Class.fromValue(classValue)
        classOrError.map(validatedClass ⇒ Action(userID, userName, action, clas = Some(validatedClass)))
      }
      .leftMap(CommandCreationException)

  def validateMovement(userID: Long, userName: String, action: String, x: Option[Int], y: Option[Int]): Either[CommandCreationException, Action] = {
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
    } yield Action(userID, userName, action, x = validatedX, y = validatedY)
  }.toRight(CommandCreationException("An error occurred when creating the movement command"))
}
